using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EarlyDocs
{
	class XmlComments
	{
		public string Text { get; protected set; }
		public bool IsEmpty {
			get {
				return String.IsNullOrEmpty(Text);
			}
		}

		public XmlComments()
		{
		}

		public XmlComments(string text)
		{
			Text = text;
		}

		public XmlComments(XElement element)
		{
			Text = element.ToString().StripOuterTags();
		}

		public override string ToString()
		{
			return ToMarkdown(Text);
		}

		private string ToMarkdown(string text, int index = 0)
		{
			StringBuilder output = new StringBuilder();

			while(index < text.Length)
			{
				if(AtTag(text, index, "see"))
				{
					string tag = GetSingleTag(text, index);
					output.Append(TagToTypeLink(tag));
					index += tag.Length;
				}
				else if(AtTag(text, index, "para"))
				{
					string tag = GetDoubleTag(text, index, "para");
					index += tag.Length + 1;
					tag = tag.Replace("<para>", "").Replace("</para>", "").Trim();
					output.Append(ToMarkdown(tag));
					output.Append("\n\n");
					if(AllWhiteSpaceToNextTag(text, index, "para"))
					{
						index = SkipWhitespace(text, index);
					}
				}
				else if(AtTag(text, index, "list"))
				{
					string tag = GetDoubleTag(text, index, "list");
					index += tag.Length + 1;
					output.Append(TagToList(tag));
				}
				else
				{
					output.Append(text[index]);
					index++;
				}
			}

			return output.ToString().Trim('\n');
		}

		private bool AtTag(string text, int index, string tagName)
		{
			if(text.Length <= index + tagName.Length)
				return false;
			return (text.Substring(index, tagName.Length + 1) == "<" + tagName);
		}

		private string GetSingleTag(string text, int index)
		{
			int endIndex = text.IndexOf("/>", index) + 1;
			return text.Substring(index, endIndex - index + 1);
		}

		private string GetDoubleTag(string text, int index, string tagName)
		{
			string tag = "</" + tagName + ">";
			int endIndex = text.IndexOf(tag, index) + tag.Length;
			return text.Substring(index, endIndex - index);
		}

		private string TagToTypeLink(string tag)
		{
			XElement xml = XElement.Parse(tag);
			string cref = xml.Attribute("cref").Value;
			string typeName = cref.Substring(cref.LastIndexOf('.') + 1);
			return String.Format("[{0}]({0}.md)", typeName);
		}

		//todo: support "term" and "description" tags in "listheader" and "item"
		//todo: ? support multiple "listheader" ?
		//todo: support numbered lists and table
		private string TagToList(string tag)
		{
			StringBuilder output = new StringBuilder();
			XElement element = XElement.Parse(tag);
			XElement header = element.Descendants().FirstOrDefault(d => d.Name == "listheader");
			if(header != null)
			{
				output.Append(header.Value.Trim() + "  \n");
			}
			foreach(XElement item in element.Descendants().Where(d => d.Name == "item"))
			{
				output.Append(String.Format("* {0}  \n", item.Value.Trim()));
			}
			return output.ToString();
		}

		private bool AllWhiteSpaceToNextTag(string text, int index, string tagName)
		{
			if(index >= text.Length)
				return false;
			int tagIndex = text.IndexOf("<" + tagName, index);
			if(tagIndex == -1)
				return false;
			string between = text.Substring(index, tagIndex - index);
			return (String.IsNullOrEmpty(between.Trim()));
		}

		private int SkipWhitespace(string text, int index)
		{
			while(index < text.Length && text[index].IsWhitespace())
			{
				index++;
			}
			return index;
		}
	}
}
