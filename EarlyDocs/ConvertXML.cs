using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using WithoutHaste.DataFiles.DotNet;
using WithoutHaste.DataFiles.Markdown;

namespace EarlyDocs
{
	internal static class Ext
	{
		public const string MD = ".md";
	}

	class ConvertXML
	{
		private Dictionary<string, XmlType> typeNameToType = new Dictionary<string, XmlType>();
		private List<XmlType> ExceptionTypes {
			get {
				return typeNameToType.Values.Where(t => t.IsException).ToList();
			}
		}
		private List<XmlType> NormalTypes {
			get {
				return typeNameToType.Values.Where(t => !t.IsAbstract && !t.IsStatic && !t.IsInterface && !t.IsException && !t.IsEnum).ToList();
			}
		}
		private List<XmlType> StaticTypes {
			get {
				return typeNameToType.Values.Where(t => t.IsStatic && !t.IsInterface && !t.IsException && !t.IsEnum).ToList();
			}
		}
		private List<XmlType> InterfaceTypes {
			get {
				return typeNameToType.Values.Where(t => t.IsInterface && !t.IsException).ToList();
			}
		}
		private List<XmlType> EnumTypes {
			get {
				return typeNameToType.Values.Where(t => t.IsEnum && !t.IsException).ToList();
			}
		}
		private List<XmlType> AbstractTypes {
			get {
				return typeNameToType.Values.Where(t => t.IsAbstract && !t.IsException).ToList();
			}
		}

		public ConvertXML(string dllFilename, string xmlDocumentationFilename, string outputDirectory)
		{
			bool emptyOutputDirectoryFirst = true;

			DotNetDocumentationFile xmlDocumentation = new DotNetDocumentationFile(xmlDocumentationFilename);
			xmlDocumentation.AddAssemblyInfo(dllFilename);





			LoadXML(xmlDocumentationFilename);
			LoadAssembly(dllFilename);

			PrepareOutputDirectory(outputDirectory, emptyOutputDirectoryFirst);
			//foreach(XmlType type in typeNameToType.Values)
			//{
			//	Save(type.ToMarkdownFile(), outputDirectory, type.Name + Ext.MD);
			//}
			foreach(DotNetType type in xmlDocumentation.Types)
			{
				Save(type, outputDirectory, type.Name.LocalName + Ext.MD);
			}
			Save(GenerateTableOfContents(xmlDocumentation), outputDirectory, "TableOfContents" + Ext.MD);
		}

		private void PrepareOutputDirectory(string outputDirectory, bool emptyOutputDirectoryFirst)
		{
			if(!Directory.Exists(outputDirectory))
			{
				Directory.CreateDirectory(outputDirectory);
			}
			if(emptyOutputDirectoryFirst)
			{
				foreach(FileInfo file in (new DirectoryInfo(outputDirectory)).GetFiles())
				{
					file.Delete();
				}
			}
		}

		private void LoadXML(string filename)
		{
			XDocument doc = XDocument.Load(filename);

			foreach(XElement element in doc.Root.Elements())
			{
				if(element.Name == "members")
				{
					LoadMembers(element);
				}
			}
		}

		private void LoadAssembly(string dll)
		{
			Assembly a = Assembly.LoadFrom(dll);
			foreach(TypeInfo typeInfo in a.DefinedTypes)
			{
				XmlType type = FindType(typeInfo.FullName.Replace('+', '.'));
				if(type == null) continue;

				type.Apply(typeInfo);
			}
		}

		private void LoadMembers(XElement members)
		{
			foreach(XElement element in members.Elements())
			{
				if(element.Attribute("name") == null)
					continue;
				if(element.Attribute("name").Value.StartsWith("T:"))
				{
					LoadType(element);
					continue;
				}
				else if(element.Attribute("name").Value.StartsWith("M:"))
				{
					LoadMethod(element);
					continue;
				}
				else if(element.Attribute("name").Value.StartsWith("F:"))
				{
					LoadField(element);
					continue;
				}
				else if(element.Attribute("name").Value.StartsWith("P:"))
				{
					LoadProperty(element);
					continue;
				}
				else if(element.Attribute("name").Value.StartsWith("E:"))
				{
					LoadEvent(element);
					continue;
				}
			}
		}

		private void LoadType(XElement element)
		{
			XmlType type = new XmlType(element);
			if(typeNameToType.ContainsKey(type.Assembly)) //check for nested type
			{
				typeNameToType[type.Assembly].Add(type);
				return;
			}
			typeNameToType[type.TypeName] = type;
		}

		private void LoadMethod(XElement element)
		{
			XmlMethod member = new XmlMethod(element);
			XmlType parent = FindType(member.TypeName);
			if(parent == null)
				return;
			parent.Add(member);
		}

		private void LoadField(XElement element)
		{
			XmlField field = new XmlField(element);
			XmlType parent = FindType(field.ParentTypeName);
			if(parent == null)
				return;
			parent.Add(field);
		}

		private void LoadProperty(XElement element)
		{
			XmlProperty property = new XmlProperty(element);
			XmlType parent = FindType(property.ParentTypeName);
			if(parent == null)
				return;
			parent.Add(property);
		}

		private void LoadEvent(XElement element)
		{
			XmlEvent e = new XmlEvent(element);
			XmlType parent = FindType(e.ParentTypeName);
			if(parent == null)
				return;
			parent.Add(e);
		}

		private XmlType FindType(string name)
		{
			foreach(XmlType type in typeNameToType.Values)
			{
				if(type.TypeName == name)
					return type;
				foreach(XmlType subType in type.Types)
				{
					if(subType.TypeName == name)
						return subType;
				}
			}
			return null;
		}

		[ObsoleteAttribute("Replace with Save(MarkdownFile...)")]
		private void Save(string text, string directory, string filename)
		{
			using(StreamWriter writer = new StreamWriter(Path.Combine(directory, filename)))
			{
				writer.Write(text);
			}
		}

		private void Save(MarkdownFile markdown, string directory, string filename)
		{
			using(StreamWriter writer = new StreamWriter(Path.Combine(directory, filename)))
			{
				writer.Write(markdown.ToMarkdown());
			}
		}

		private void Save(DotNetType type, string directory, string filename)
		{
			//todo: move formatting of filename to central location that links can use
			filename = filename.Replace("<", "_").Replace(">", "_").Replace(",", "_");

			using(StreamWriter writer = new StreamWriter(Path.Combine(directory, filename)))
			{
				writer.Write(type.ToMarkdownFile().ToMarkdown());
			}
		}

		private MarkdownFile GenerateTableOfContents(DotNetDocumentationFile xmlDocumentation)
		{
			MarkdownFile markdown = new MarkdownFile();
			MarkdownSection section = markdown.AddSection("Contents");
			AddTableOfContentsSection(section, "Concrete Types", xmlDocumentation.Types.Where(t => t.Category == TypeCategory.Normal).ToList());
			AddTableOfContentsSection(section, "Static Types", xmlDocumentation.Types.Where(t => t.Category == TypeCategory.Static).ToList());
			AddTableOfContentsSection(section, "Abstract Types", xmlDocumentation.Types.Where(t => t.Category == TypeCategory.Abstract).ToList());
			AddTableOfContentsSection(section, "Interfaces", xmlDocumentation.Types.Where(t => t.Category == TypeCategory.Interface).ToList());
			AddTableOfContentsSection(section, "Enums", xmlDocumentation.Types.Where(t => t.Category == TypeCategory.Enum).ToList());
			AddTableOfContentsSection(section, "Exceptions", xmlDocumentation.Types.Where(t => t.Category == TypeCategory.Exception).ToList());

			return markdown;
		}

		private void AddTableOfContentsSection(MarkdownSection parent, string header, List<DotNetType> types)
		{
			if(types.Count == 0) return;

			MarkdownSection section = parent.AddSection(header);
			foreach(DotNetType type in types.OrderBy(t => t.Name.LocalName))
			{
				section.AddInLine(new MarkdownInlineLink(type.Name.LocalName, type.Name.LocalName + Ext.MD));
				section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(type.SummaryComments));
			}
		}
	}
}
