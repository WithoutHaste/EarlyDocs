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
			if(DataTypeName.Contains("Nullable"))
			{
				//FullName example: System.Nullable`1[[System.Drawing.Color, System.Drawing, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a]]
				string innerType = propertyInfo.PropertyType.FullName;
				innerType = innerType.Substring(innerType.IndexOf("[[") + 2);
				innerType = innerType.Substring(0, innerType.IndexOf(","));
				DataTypeName = innerType + "?";
			}
		}
	}
}
