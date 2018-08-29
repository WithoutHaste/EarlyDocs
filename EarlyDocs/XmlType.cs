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
		public bool IsStatic { get; protected set; }

		public List<XmlField> Fields = new List<XmlField>();
		public List<XmlMethod> Methods = new List<XmlMethod>();
		public List<XmlType> Types = new List<XmlType>();
		public List<XmlEnum> Enums {
			get {
				return Types.OfType<XmlEnum>().ToList();
			}
		}

		public XmlType(XElement element) : base(element)
		{
			TypeName = element.Attribute("name")?.Value.Substring(2);
			ParseAssembly(TypeName);
			ParseName(TypeName);
		}

		public static XmlType Factory(XElement element)
		{
			if(element.Descendants().Any(d => d.Name == "enum"))
			{
				return new XmlEnum(element);
			}
			return new XmlType(element);
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

		public void Add(XmlField field)
		{
			Fields.Add(field);
		}

		public void Add(XmlMethod method)
		{
			Methods.Add(method);
		}

		public void Add(XmlType type)
		{
			Types.Add(type);
		}

		public virtual string ToMarkdown()
		{
			StringBuilder output = new StringBuilder();

			output.Append(String.Format("# {0}\n\n", Name));
			output.Append(String.Format("{0}\n\n", Summary));

			if(Enums.Count > 0)
			{
				output.Append(String.Format("## Enums\n\n"));
				foreach(XmlEnum e in Enums.OrderBy(m => m.Name))
				{
					output.Append(e.ToMarkdown());
				}
			}

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
