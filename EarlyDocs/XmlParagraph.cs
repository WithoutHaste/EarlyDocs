using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithoutHaste.DataFiles.Markdown;

namespace EarlyDocs
{
	class XmlParagraph : IXmlInComments
	{
		public List<IXmlInComments> Elements = new List<IXmlInComments>();

		public XmlParagraph()
		{
		}

		public XmlParagraph(IXmlInComments[] elements)
		{
			Elements.AddRange(elements);
		}

		public XmlParagraph(string text)
		{
			text = text.Trim();
			if(XmlComments.AtTag(text, 0, "para"))
			{
				text = text.StripOuterTags().Trim();
			}
			Elements.AddRange(XmlComments.ParseText(text));
		}

		public void Add(IXmlInComments element)
		{
			Elements.Add(element);
		}

		public void Add(IXmlInComments[] elements)
		{
			Elements.AddRange(elements);
		}

		public IMarkdownInSection ToMarkdownInSection()
		{
			return new MarkdownParagraph(Elements.Select(e => e.ToMarkdownInSection()).OfType<IMarkdownInLine>().ToArray()); //todo: limiting by type seems like error in class inheritance hierarchy
		}
	}
}
