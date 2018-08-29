using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EarlyDocs
{
	class ConvertXML
	{
		private Dictionary<string, XmlType> typeNameToType = new Dictionary<string, XmlType>();

		public ConvertXML(string filename, string outputDirectory)
		{
			Load(filename);

			if(!Directory.Exists(outputDirectory))
			{
				Directory.CreateDirectory(outputDirectory);
			}
			foreach(XmlType type in typeNameToType.Values)
			{
				Save(type.ToMarkdown(), outputDirectory, type.Name + ".md");
			}
			Save(GenerateTableOfContents(), outputDirectory, "TableOfContents.md");
		}

		private void Load(string filename)
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
			}
		}

		private void LoadType(XElement element)
		{
			XmlType type = new XmlType(element);
			typeNameToType[type.TypeName] = type;
		}

		private void LoadMethod(XElement element)
		{
			XmlMethod member = new XmlMethod(element);
			if(!typeNameToType.ContainsKey(member.TypeName))
				throw new Exception("Missing documenation for Type: " + member.TypeName);
			typeNameToType[member.TypeName].Add(member);
		}

		private void LoadField(XElement element)
		{
			XmlField field = new XmlField(element);
			if(!typeNameToType.ContainsKey(field.TypeName))
				throw new Exception("Missing documenation for Type: " + field.TypeName);
			typeNameToType[field.TypeName].Add(field);
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
			foreach(XmlType type in typeNameToType.Values.OrderBy(t => t.Name))
			{
				output.Append(String.Format("[{0}]({1}.md)  \n{2}\n\n", type.Name, type.Name, type.Summary));
			}

			return output.ToString();
		}
	}
}
