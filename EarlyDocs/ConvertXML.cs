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
		private List<XmlType> NormalTypes {
			get {
				return typeNameToType.Values.Where(t => !t.IsAbstract && !t.IsStatic && !t.IsInterface).ToList();
			}
		}
		private List<XmlType> StaticTypes {
			get {
				return typeNameToType.Values.Where(t => t.IsStatic && !t.IsInterface).ToList();
			}
		}
		private List<XmlType> InterfaceTypes {
			get {
				return typeNameToType.Values.Where(t => t.IsInterface).ToList();
			}
		}
		private List<XmlType> AbstractTypes {
			get {
				return typeNameToType.Values.Where(t => t.IsAbstract).ToList();
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
				if(element.Attribute("name").Value.StartsWith("M:"))
				{
					LoadMethod(element);
					continue;
				}
				if(element.Attribute("name").Value.StartsWith("F:"))
				{
					LoadField(element);
					continue;
				}
				if(element.Attribute("name").Value.StartsWith("P:"))
				{
					LoadProperty(element);
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
				throw new Exception("Missing documenation for Type: " + member.TypeName);
			parent.Add(member);
		}

		private void LoadField(XElement element)
		{
			XmlField field = new XmlField(element);
			XmlType parent = FindType(field.ParentTypeName);
			if(parent == null)
				throw new Exception("Missing documenation for Type: " + field.ParentTypeName);
			parent.Add(field);
		}

		private void LoadProperty(XElement element)
		{
			XmlProperty property = new XmlProperty(element);
			XmlType parent = FindType(property.ParentTypeName);
			if(parent == null)
				throw new Exception("Missing documenation for Type: " + property.ParentTypeName);
			parent.Add(property);
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
			output.Append("## Abstract Types\n\n");
			foreach(XmlType type in AbstractTypes.OrderBy(t => t.Name))
			{
				output.Append(String.Format("[{0}]({1}.md)  \n{2}\n\n", type.Name, type.Name, type.Summary));
			}
			output.Append("## Types\n\n");
			foreach(XmlType type in NormalTypes.OrderBy(t => t.Name))
			{
				output.Append(String.Format("[{0}]({1}.md)  \n{2}\n\n", type.Name, type.Name, type.Summary));
			}
			output.Append("## Static Types\n\n");
			foreach(XmlType type in StaticTypes.OrderBy(t => t.Name))
			{
				output.Append(String.Format("[{0}]({1}.md)  \n{2}\n\n", type.Name, type.Name, type.Summary));
			}
			output.Append("## Interfaces\n\n");
			foreach(XmlType type in InterfaceTypes.OrderBy(t => t.Name))
			{
				output.Append(String.Format("[{0}]({1}.md)  \n{2}\n\n", type.Name, type.Name, type.Summary));
			}

			return output.ToString();
		}
	}
}
