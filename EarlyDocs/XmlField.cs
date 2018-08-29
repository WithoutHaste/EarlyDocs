using System;
using System.Collections.Generic;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EarlyDocs
{
	class XmlField : XmlMember
	{
		private readonly FieldAttributes CONSTANT_FIELDATTRIBUTES = FieldAttributes.Static | FieldAttributes.InitOnly;

		public string ParentTypeName { get; protected set; }
		public string Assembly { get; protected set; }
		public string Name { get; protected set; }
		public bool IsConstant { get; protected set; }
		public string DataTypeName { get; protected set; }		

		public XmlField(XElement element) : base(element)
		{
			string name = element.Attribute("name").Value.Substring(2);
			ParseTypeNameAndAssembly(name);
			ParseMemberName(name);
		}

		private void ParseTypeNameAndAssembly(string name)
		{
			string[] fields = name.Split('.');
			ParentTypeName = String.Join(".", fields.Take(fields.Length - 1).ToArray());
			Assembly = String.Join(".", fields.Take(fields.Length - 2).ToArray());
		}

		private void ParseMemberName(string name)
		{
			string[] fields = name.Split('.');
			Name = fields.Last();
		}

		public void Apply(FieldInfo fieldInfo)
		{
			IsConstant = ((fieldInfo.Attributes & CONSTANT_FIELDATTRIBUTES) == CONSTANT_FIELDATTRIBUTES);
			DataTypeName = fieldInfo.FieldType.Name;
		}

		public string ToMarkdown(int indent)
		{
			StringBuilder output = new StringBuilder();

			output.Append(String.Format("{0} {1} {2}\n\n", new String('#', indent), DataTypeName, Name));
			if(!String.IsNullOrEmpty(Summary))
			{
				output.Append(String.Format("{0}\n\n", Summary));
			}

			return output.ToString();
		}
	}
}
