using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WithoutHaste.DataFiles.Markdown;

namespace EarlyDocs
{
	class XmlComments
	{
		public List<IXmlInComments> Elements = new List<IXmlInComments>();
		public bool IsEmpty { get { return (Elements.Count == 0); } }

		public XmlComments()
		{
		}

		public XmlComments(string text)
		{
			Elements.AddRange(ParseText(text));
		}

		public XmlComments(XElement element)
		{
			string text = element.ToString().StripOuterTags().Trim();
			Elements.AddRange(ParseText(text));
		}

		//todo: look at refactor https://stackoverflow.com/questions/11555534/reading-from-a-stream-with-mixed-xml-and-plain-text
		//which shows parsing text/xml mix

		public static IXmlInComments[] ParseText(string text, int index=0)
		{
			List<IXmlInComments> elements = new List<IXmlInComments>();

			while(index < text.Length)
			{
				if(AtTag(text, index, "see"))
				{
					string tag = GetSingleTag(text, index);
					elements.Add(new XmlLink(tag));
					index += tag.Length;
				}
				else if(AtTag(text, index, "para"))
				{
					string tag = GetDoubleTag(text, index, "para");
					index += tag.Length + 1;
					elements.Add(new XmlParagraph(tag));
					if(AllWhiteSpaceToNextTag(text, index, "para"))
					{
						index = SkipWhitespace(text, index);
					}
				}
				else if(AtTag(text, index, "list"))
				{
					string tag = GetDoubleTag(text, index, "list");
					index += tag.Length + 1;
					elements.Add(new XmlList(tag));
				}
				else if(AtTag(text, index, "code"))
				{
					string tag = GetDoubleTag(text, index, "code");
					index += tag.Length + 1;
					elements.Add(new XmlCodeBlock(tag));
				}
				else if(AtCDATATag(text, index))
				{
					string tag = GetCDATATag(text, index);
					index += tag.Length + 1;
					elements.Add(new XmlCodeBlock(tag));
				}
				else
				{
					elements.Add(new XmlText(text[index].ToString())); //todo: inefficient to save each char separately
					index++;
				}
			}

			return elements.ToArray();
		}

		public IMarkdownInSection[] ToMarkdown()
		{
			return Elements.Select(e => e.ToMarkdownInSection()).ToArray();
		}

		//todo: move these static methods to StringExtensions or something like it

		public static bool AtTag(string text, int index, string tagName)
		{
			if(text.Length <= index + tagName.Length)
				return false;
			return (text.Substring(index, tagName.Length + 1) == "<" + tagName);
		}

		public static bool AtCDATATag(string text, int index)
		{
			return AtTag(text, index, "![CDATA[");
		}

		private static string GetSingleTag(string text, int index)
		{
			int endIndex = text.IndexOf("/>", index) + 1;
			return text.Substring(index, endIndex - index + 1);
		}

		private static string GetDoubleTag(string text, int index, string tagName)
		{
			string tag = "</" + tagName + ">";
			int endIndex = text.IndexOf(tag, index) + tag.Length;
			return text.Substring(index, endIndex - index);
		}

		private static string GetCDATATag(string text, int index)
		{
			string tag = "]]>";
			int endIndex = text.IndexOf(tag, index) + tag.Length;
			return text.Substring(index, endIndex - index);
		}
		
		private static bool AllWhiteSpaceToNextTag(string text, int index, string tagName)
		{
			if(index >= text.Length)
				return false;
			int tagIndex = text.IndexOf("<" + tagName, index);
			if(tagIndex == -1)
				return false;
			string between = text.Substring(index, tagIndex - index);
			return (String.IsNullOrEmpty(between.Trim()));
		}

		private static int SkipWhitespace(string text, int index)
		{
			while(index < text.Length && text[index].IsWhitespace())
			{
				index++;
			}
			return index;
		}
	}
}
