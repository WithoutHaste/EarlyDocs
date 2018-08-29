using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EarlyDocs
{
	class XmlProperty : XmlField
	{
		public XmlProperty(XElement element) : base(element)
		{
		}

		public void Apply(PropertyInfo propertyInfo)
		{
			IsConstant = false;
			DataTypeName = propertyInfo.PropertyType.Name;
		}
	}
}
