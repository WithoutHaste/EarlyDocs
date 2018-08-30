using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EarlyDocs
{
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

		public ConvertXML(string dll, string filename, string outputDirectory)
		{
			LoadXML(filename);
			LoadAssembly(dll);

			if(!Directory.Exists(outputDirectory))
			{
				Directory.CreateDirectory(outputDirectory);
			}
			foreach(XmlType type in typeNameToType.Values)
			{
				Save(type.ToMarkdown(1), outputDirectory, type.Name + ".md");
			}
			Save(GenerateTableOfContents(), outputDirectory, "TableOfContents.md");
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

		private void Save(string text, string directory, string filename)
		{
			using(StreamWriter writer = new StreamWriter(Path.Combine(directory, filename)))
			{
				writer.Write(text);
			}
		}

		private string GenerateTableOfContents()
		{
			StringBuilder output = new StringBuilder();

			output.Append("# Contents\n\n");
			GenerateTableOfContentsSection("Abstract Types", AbstractTypes, output);
			GenerateTableOfContentsSection("Types", NormalTypes, output);
			GenerateTableOfContentsSection("Static Types", StaticTypes, output);
			GenerateTableOfContentsSection("Interfaces", InterfaceTypes, output);
			GenerateTableOfContentsSection("Enums", EnumTypes, output);
			GenerateTableOfContentsSection("Exceptions", ExceptionTypes, output);

			return output.ToString();
		}

		private void GenerateTableOfContentsSection(string header, List<XmlType> types, StringBuilder output)
		{
			if(types.Count == 0) return;

			output.Append(String.Format("## {0}\n\n", header));
			foreach(XmlType type in types.OrderBy(t => t.Name))
			{
				output.Append(String.Format("[{0}]({1}.md)  \n{2}\n\n", type.Name, type.Name, type.Summary));
			}
		}
	}
}
