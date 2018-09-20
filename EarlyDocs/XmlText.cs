using WithoutHaste.DataFiles.Markdown;

namespace EarlyDocs
{
	class XmlText : IXmlInComments, IXmlInList
	{
		public readonly string Text;

		public XmlText(string text)
		{
			Text = text;
		}

		public IMarkdownInList ToMarkdownInList()
		{
			return new MarkdownLine(Text);
		}

		public IMarkdownInSection ToMarkdownInSection()
		{
			return new MarkdownText(Text);
		}
	}
}
