using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithoutHaste.DataFiles.DotNet;
using WithoutHaste.DataFiles.Markdown;

namespace EarlyDocs
{
	public static class ConvertDotNet
	{
		public static List<IMarkdownInSection> DotNetCommentGroupToMarkdown(DotNetCommentGroup group)
		{
			return DotNetCommentsToMarkdown(group.Comments);
		}

		public static List<IMarkdownInSection> DotNetCommentsToMarkdown(List<DotNetComment> list)
		{
			//todo: expand support
			List<IMarkdownInSection> markdown = new List<IMarkdownInSection>();

			foreach(DotNetComment comment in list)
			{
				if(comment is DotNetCommentGroup)
				{
					markdown.AddRange(DotNetCommentGroupToMarkdown(comment as DotNetCommentGroup));
				}
				else if(comment is DotNetCommentText)
				{
					markdown.Add(new MarkdownParagraph((comment as DotNetCommentText).Text));
				}
			}

			return markdown;
		}
	}
}
