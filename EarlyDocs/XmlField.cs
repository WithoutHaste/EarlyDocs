using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EarlyDocs
{
	class XmlField : XmlMember
	{
		public string TypeName { get; protected set; }
		public string Assembly { get; protected set; }
		public string Name { get; protected set; }
		public bool IsConstant { get; protected set; }

		public XmlField(XElement element) : base(element)
		{
			string name = element.Attribute("name").Value.Substring(2);
			ParseTypeNameAndAssembly(name);
			ParseMemberName(name);
			IsConstant = element.Descendants().Any(d => d.Name == "constant");
		}

		private void ParseTypeNameAndAssembly(string name)
		{
			string[] fields = name.Split('.');
			TypeName = String.Join(".", fields.Take(fields.Length - 1).ToArray());
			Assembly = String.Join(".", fields.Take(fields.Length - 2).ToArray());
		}

		private void ParseMemberName(string name)
		{
			string[] fields = name.Split('.');
			Name = fields.Last();
		}

		public string ToMarkdown(int indent)
		{
			StringBuilder output = new StringBuilder();

			output.Append(String.Format("{0} {1}\n\n", new String('#', indent), Name));
			if(!String.IsNullOrEmpty(Summary))
			{
				output.Append(String.Format("{0}\n\n", Summary));
			}

			return output.ToString();
		}
	}
}
