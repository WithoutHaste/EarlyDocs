using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WithoutHaste.DataFiles.Markdown;

namespace EarlyDocs
{
	class XmlList : IXmlInList, IXmlInComments
	{
		public XmlListItem Header;
		public List<IXmlInList> Elements = new List<IXmlInList>();
		public readonly bool IsNumbered;

		public XmlList(bool isNumbered=false)
		{
			IsNumbered = isNumbered;
		}

		public XmlList(IXmlInList[] elements, bool isNumbered=false)
		{
			Elements.AddRange(elements);
			IsNumbered = isNumbered;
		}

		//todo: support "term" and "description" tags in "listheader" and "item"
		//todo: ? support multiple "listheader" ?
		//todo: support numbered lists and table
		public XmlList(string tag)
		{
			//todo: translate to DotNet structure
			//XElement element = XElement.Parse(tag); //expects <list> tag
			//IsNumbered = (element.Attribute("type").Value == "number");
			//XElement header = element.Descendants().FirstOrDefault(d => d.Name == "listheader");
			//if(header != null)
			//{
			//	Header = new XmlListItem(header);
			//}
			//foreach(XElement item in element.Descendants().Where(d => d.Name == "item"))
			//{
			//	Elements.Add(new XmlListItem(item));
			//}
		}

		public void Add(IXmlInList element)
		{
			Elements.Add(element);
		}

		public void Add(IXmlInList[] elements)
		{
			Elements.AddRange(elements);
		}

		public IMarkdownInSection ToMarkdownInSection()
		{
			return ToMarkdownInList();
		}

		public IMarkdownInList ToMarkdownInList()
		{
			MarkdownList list = new MarkdownList(IsNumbered);

			if(Header != null)
				list.Add(Header.ToMarkdownInList());
			foreach(IXmlInList element in Elements)
			{
				list.Add(element.ToMarkdownInList());
			}

			return list;
		}
	}
}
