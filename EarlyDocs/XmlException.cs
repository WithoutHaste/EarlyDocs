using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EarlyDocs
{
	class XmlException
	{
		public string ExceptionType { get; protected set; }
		public string Description { get; protected set; }

		public XmlException(XElement element)
		{
			ExceptionType = element.Attribute("cref").Value.LastTerm();
			Description = element.Value;
		}
	}
}
