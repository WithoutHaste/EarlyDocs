using System;
using WithoutHaste.DataFiles.Markdown;

namespace EarlyDocs
{
	/// <summary>
	/// An object that can be added to an XmlComments.
	/// </summary>
	interface IXmlInComments
	{
		IMarkdownInSection ToMarkdownInSection();
	}
}
