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
			List<IMarkdownInSection> markdown = new List<IMarkdownInSection>();

			foreach(DotNetComment comment in list)
			{
				markdown.AddRange(DotNetCommentsToMarkdown(comment));
			}

			return markdown;
		}

		public static List<IMarkdownInSection> DotNetCommentsToMarkdown(DotNetComment comment)
		{
			List<IMarkdownInSection> markdown = new List<IMarkdownInSection>();

			if(comment is DotNetCommentGroup)
			{
				markdown.AddRange(DotNetCommentGroupToMarkdown(comment as DotNetCommentGroup));
			}
			else if(comment is DotNetCommentText)
			{
				string text = (comment as DotNetCommentText).Text;
				markdown.AddRange(text.Split('\n').Select(t => new MarkdownLine(t)).ToArray());
			}

			return markdown;
		}
	}
}
