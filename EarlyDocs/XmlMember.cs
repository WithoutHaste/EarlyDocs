using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EarlyDocs
{
	abstract class XmlMember
	{
		private XElement xml;

		public string Summary { get; protected set; }

		public XmlMember(XElement element)
		{
			xml = element;

			ParseSummary(element);
		}

		public void ParseSummary(XElement element)
		{
			foreach(XElement child in element.Elements())
			{
				if(child.Name == "summary")
				{
					Summary = child.Value.Trim();
				}
			}
		}
	}
}
