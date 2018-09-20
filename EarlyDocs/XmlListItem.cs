using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WithoutHaste.DataFiles.Markdown;

namespace EarlyDocs
{
	class XmlListItem : IXmlInList
	{
		public readonly string Term;
		public readonly string Description;

		public XmlListItem(string term, string description = null)
		{
			Term = term;
			Description = description;
		}

		public XmlListItem(XElement item) //expects <listheader> or <item> tag
		{
			string term = null;
			string description = null;

			if(item.Descendants().Any(d => d.Name == "term"))
			{
				term = item.Descendants().First(d => d.Name == "term").Value.Trim();
				if(item.Descendants().Any(d => d.Name == "description"))
				{
					description = item.Descendants().First(d => d.Name == "description").Value.Trim();
				}
			}
			term = item.Value.Trim();

			Term = term;
			Description = description;
		}

		public IMarkdownInList ToMarkdownInList()
		{
			if(String.IsNullOrEmpty(Description))
				return new MarkdownLine(Term);
			return new MarkdownLine(String.Format("{0}: {1}", Term, Description));
		}
	}
}
