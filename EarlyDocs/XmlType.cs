using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EarlyDocs
{
	class XmlType : XmlMember
	{
		public string TypeName { get; protected set; }
		public string Assembly { get; protected set; }
		public string Name { get; protected set; }

		public List<XmlMethod> Methods = new List<XmlMethod>();
		public List<XmlField> Fields = new List<XmlField>();

		public XmlType(XElement element) : base(element)
		{
			TypeName = element.Attribute("name")?.Value.Substring(2);
			ParseAssembly(TypeName);
			ParseName(TypeName);
		}

		private void ParseAssembly(string fullName)
		{
			string[] fields = fullName.Split('.');
			Assembly = String.Join(".", fields.Take(fields.Length - 1).ToArray());
		}

		private void ParseName(string fullName)
		{
			string[] fields = fullName.Split('.');
			Name = fields.Last();
		}

		public void Add(XmlMethod method)
		{
			Methods.Add(method);
		}

		public void Add(XmlField field)
		{
			Fields.Add(field);
		}

		public string ToMarkdown()
		{
			StringBuilder output = new StringBuilder();

			output.Append(String.Format("# {0}\n\n", Name));
			output.Append(String.Format("{0}\n\n", Summary));

			if(Fields.Count > 0)
			{
				output.Append(String.Format("## Fields\n\n"));
				foreach(XmlField field in Fields.OrderBy(m => m.Name))
				{
					output.Append(field.ToMarkdown());
				}
			}

			if(Methods.Count > 0)
			{
				output.Append(String.Format("## Methods\n\n"));
				foreach(XmlMethod method in Methods.OrderBy(m => m.Name))
				{
					output.Append(method.ToMarkdown());
				}
			}

			return output.ToString();
		}
	}
}
