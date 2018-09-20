using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using WithoutHaste.DataFiles.Markdown;

namespace EarlyDocs
{
	class XmlParam
	{
		public string Assembly { get; protected set; }
		public string FullTypeName { get; protected set; }
		public string TypeName {
			get {
				if(FullTypeName.StartsWith(Assembly))
					return FullTypeName.Substring(Assembly.Length + 1);
				return FullTypeName;
			}
		}
		public string ShortTypeName {
			get {
				return FullTypeName.LastTerm();
			}
		}
		public string Name { get; protected set; }
		public string Description { get; protected set; }

		public XmlParam(string typeName, string assembly)
		{
			Assembly = assembly;
			FullTypeName = typeName;
		}

		public void Apply(ParameterInfo parameterInfo)
		{
			Name = parameterInfo.Name;
		}

		public void Apply(XElement element)
		{
			Description = element.Value;
		}

		public MarkdownText ToMarkdown()
		{
			string text = TypeName;
			if(!String.IsNullOrEmpty(Name)) text += " " + Name;
			return new MarkdownText(text);
		}

	}
}
