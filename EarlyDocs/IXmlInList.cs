using WithoutHaste.DataFiles.Markdown;

namespace EarlyDocs
{
	/// <summary>
	/// An object that can be added to an XmlList.
	/// </summary>
	interface IXmlInList
	{
		IMarkdownInList ToMarkdownInList();
	}
}
