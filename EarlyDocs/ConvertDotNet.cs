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
			List<IMarkdownInSection> result = DotNetCommentsToMarkdown(group.Comments);
			result.Add(new MarkdownLine()); //because comment groups are like paragraphs, with space in between
			return result;
		}

		/// <summary>
		/// Returns the first line of comments produced by the group.
		/// Intended for groups that boil down to a single line.
		/// </summary>
		public static List<IMarkdownInLine> DotNetCommentGroupToMarkdownLine(DotNetCommentGroup group)
		{
			List<IMarkdownInSection> inSectionList = DotNetCommentGroupToMarkdown(group);
			List<IMarkdownInLine> line = new List<IMarkdownInLine>();

			foreach(IMarkdownInSection inSection in inSectionList)
			{
				if(inSection is MarkdownLine)
				{
					line.AddRange((inSection as MarkdownLine).Elements);
					return line;
				}
				if(inSection is IMarkdownInLine)
				{
					line.Add(inSection as IMarkdownInLine);
				}
				else
				{
					return line;
				}
			}

			return line;
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
			else if(comment is DotNetCommentList)
			{
				markdown.Add(DotNetCommentsToMarkdown(comment as DotNetCommentList));
			}
			else if(comment is DotNetCommentTable)
			{
				markdown.Add(DotNetCommentsToMarkdown(comment as DotNetCommentTable));
			}

			return markdown;
		}

		public static MarkdownList DotNetCommentsToMarkdown(DotNetCommentList commentList)
		{
			MarkdownList markdownList = new MarkdownList(isNumbered: commentList.IsNumbered);

			foreach(DotNetCommentListItem commentItem in commentList.Items)
			{
				string text = "";
				if(String.IsNullOrEmpty(commentItem.Term))
					text = commentItem.Description;
				else if(String.IsNullOrEmpty(commentItem.Description))
					text = "**" + commentItem.Term + "**";
				else
					text = "**" + commentItem.Term + "**: " + commentItem.Description;
				markdownList.Add(new MarkdownLine(text));
			}

			return markdownList;
		}

		public static MarkdownTable DotNetCommentsToMarkdown(DotNetCommentTable commentTable)
		{
			MarkdownTable markdownTable = new MarkdownTable();

			foreach(DotNetCommentRow commentRow in commentTable.Rows)
			{
				MarkdownTableRow markdownRow = DotNetCommentsToMarkdown(commentRow);
				markdownTable.Add(markdownRow);
			}

			return markdownTable;
		}

		public static MarkdownTableRow DotNetCommentsToMarkdown(DotNetCommentRow commentRow)
		{
			return new MarkdownTableRow(commentRow.Cells.Select(c => c.Text).ToArray());
		}

		public static MarkdownList EnumToMinimalList(DotNetType _enum)
		{
			MarkdownList list = new MarkdownList(isNumbered: false);

			foreach(DotNetField field in _enum.Fields)
			{
				list.Add(new MarkdownLine(field.ConstantValue + ": " + field.Name.LocalName));
			}

			return list;
		}
	}
}
