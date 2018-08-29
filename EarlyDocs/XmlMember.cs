﻿using System;
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
		public string MarkdownSummary {
			get {
				return ToMarkdown(Summary);
			}
		}

		public string Returns { get; protected set; }

		public XmlMember(XElement element)
		{
			xml = element;

			Parse(element);
		}

		public void Parse(XElement element)
		{
			foreach(XElement child in element.Elements())
			{
				if(child.Name == "summary")
				{
					Summary = child.ToString().Replace("<summary>", "").Replace("</summary>", "").Trim();
				}
				if(child.Name == "returns")
				{
					Returns = child.ToString().Replace("<returns>", "").Replace("</returns>", "").Trim();
				}
			}
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
				if(AtTag(text, index, "para"))
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
			while(index < text.Length && IsWhitespace(text[index]))
			{
				index++;
			}
			return index;
		}

		private bool IsWhitespace(char c)
		{
			return (c == ' ' || c == '\n' || c == '\r' || c == '\t');
		}
	}
}
