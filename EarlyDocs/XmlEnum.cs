using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EarlyDocs
{
	class XmlEnum : XmlType
	{
		public XmlEnum(XElement element) : base(element)
		{
		}

		public override string ToMarkdown(int indent)
		{
			StringBuilder output = new StringBuilder();

			output.Append(String.Format("{0} {1}\n\n", new String('#', indent), Name));
			output.Append(String.Format("{0}\n\n", Summary));

			if(Fields.Count > 0)
			{
				foreach(XmlField field in Fields)
				{
					output.Append(String.Format("* {0}: {1}  \n", field.Name, field.Summary));
				}
			}

			output.Append("\n");

			return output.ToString();
		}
	}
}
