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

		public XmlComments Summary { get; protected set; }
		public XmlComments Remarks { get; protected set; }
		public List<XmlComments> Examples = new List<XmlComments>();
		public XmlComments Returns { get; protected set; }

		public XmlMember(XElement element)
		{
			xml = element;
			Parse(element);
		}

		public void Parse(XElement element)
		{
			Summary = new XmlComments();
			Remarks = new XmlComments();
			Returns = new XmlComments();
			foreach(XElement child in element.Elements())
			{
				if(child.Name == "summary")
				{
					Summary = new XmlComments(child);
				}
				else if(child.Name == "remarks")
				{
					Remarks = new XmlComments(child);
				}
				else if(child.Name == "returns")
				{
					Returns = new XmlComments(child);
				}
				else if(child.Name == "example")
				{
					Examples.Add(new XmlComments(child));
				}
			}
		}
	}
}
