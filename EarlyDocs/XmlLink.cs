using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using WithoutHaste.DataFiles.Markdown;

namespace EarlyDocs
{
	class XmlLink : IXmlInComments
	{
		public readonly string Text;
		public readonly string Url;

		public XmlLink(string text, string url)
		{
			Text = text;
			Url = url;
		}

		/// <summary>
		/// Expects any Xml tag with a cref="type" attribute.
		/// </summary>
		public XmlLink(string tag)
		{
			XElement xml = XElement.Parse(tag);
			string cref = xml.Attribute("cref").Value;
			string url = xml.Attribute("url")?.Value;
			string typeName = cref.Substring(cref.LastIndexOf('.') + 1);

			Text = typeName;
			if(String.IsNullOrEmpty(url))
			{
				Url = typeName + Ext.MD;
				return;
			}
			if(url.EndsWithFileExtension())
			{
				Url = url;
				return;
			}
			Url = String.Format("{0}documentation /{1}{2}", url, typeName, Ext.MD);
		}

		public IMarkdownInSection ToMarkdownInSection()
		{
			return new MarkdownInlineLink(Text, Url);
		}
	}
}
