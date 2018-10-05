using System;
using System.Linq;
using System.Text;
using WithoutHaste.DataFiles.Markdown;

namespace EarlyDocs
{
	class XmlCodeBlock : IXmlInComments
	{
		public readonly string Text;
		public readonly string Language;

		public XmlCodeBlock(string text, string language=null)
		{
			Text = text;
			Language = language;
		}

		public static XmlCodeBlock FromTag(string tag) //expects <code></code> or <![CDATA[]]> tag
		{
			string language = null;

			tag = tag.StripOuterTags();
			if(XmlComments.AtCDATATag(tag, 0))
			{
				tag = tag.StripOuterCDATATags();
				language = "xml";
			}
			tag = tag.Replace("\r", "");
			if(tag.StartsWith("\n"))
			{
				tag = tag.Substring(1);
			}
			string ignoreIndent = tag.GetLeadingWhitespace();
			tag = tag.TrimEnd();

			StringBuilder builder = new StringBuilder();
			foreach(string line in tag.Split('\n'))
			{
				builder.Append(line.TrimEnd().Substring(ignoreIndent.Length) + "\n");
			}

			return new XmlCodeBlock(builder.ToString(), language);
		}

		public IMarkdownInSection ToMarkdownInSection()
		{
			return new MarkdownCodeBlock(Text, Language);
		}
	}
}
