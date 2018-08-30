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
		private readonly FieldAttributes READONLY_FIELDATTRIBUTES = FieldAttributes.Static | FieldAttributes.InitOnly;
		private readonly FieldAttributes CONSTANT_FIELDATTRIBUTES = FieldAttributes.Static | FieldAttributes.Literal;

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

		private bool FieldIsConstant(FieldAttributes attributes)
		{
			if((attributes & READONLY_FIELDATTRIBUTES) == READONLY_FIELDATTRIBUTES)
				return true;
			return ((attributes & CONSTANT_FIELDATTRIBUTES) == CONSTANT_FIELDATTRIBUTES);
		}

		public void Apply(FieldInfo fieldInfo)
		{
			IsConstant = FieldIsConstant(fieldInfo.Attributes);
			DataTypeName = fieldInfo.FieldType.Name;
		}

		public string ToMarkdown(int indent)
		{
			StringBuilder output = new StringBuilder();

			output.Append(String.Format("{0} {1} {2}\n\n", new String('#', indent), DataTypeName, Name));
			if(!Summary.IsEmpty)
			{
				output.Append(String.Format("{0}\n\n", Summary));
			}
			if(!Remarks.IsEmpty)
			{
				output.Append(String.Format("{0}\n\n", Remarks));
			}

			return output.ToString();
		}
	}
}
