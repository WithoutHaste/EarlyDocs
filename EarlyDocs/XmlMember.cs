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

		public string Summary { get; protected set; }

		public string MarkdownSummary {
			get {
				StringBuilder output = new StringBuilder();

				int index = 0;
				while(index < Summary.Length)
				{
					if(AtTag(Summary, index, "see"))
					{
						string tag = GetTag(Summary, index);
						output.Append(TagToTypeLink(tag));
						index += tag.Length;
					}
					else
					{
						output.Append(Summary[index]);
						index++;
					}
				}

				return output.ToString();
			}
		}

		public XmlMember(XElement element)
		{
			xml = element;

			ParseSummary(element);
		}

		public void ParseSummary(XElement element)
		{
			foreach(XElement child in element.Elements())
			{
				if(child.Name == "summary")
				{
					Summary = child.ToString().Replace("<summary>", "").Replace("</summary>", "").Trim();
				}
			}
		}

		private bool AtTag(string text, int index, string tagName)
		{
			if(text.Length <= index + tagName.Length)
				return false;
			return (text.Substring(index, tagName.Length + 1) == "<" + tagName);
		}

		private string GetTag(string text, int index)
		{
			int endIndex = text.IndexOf("/>", index) + 1;
			return text.Substring(index, endIndex - index + 1);
		}

		private string TagToTypeLink(string tag)
		{
			XElement xml = XElement.Parse(tag);
			string cref = xml.Attribute("cref").Value;
			string typeName = cref.Substring(cref.LastIndexOf('.') + 1);
			return String.Format("[{0}]({0}.md)", typeName);
		}
	}
}
