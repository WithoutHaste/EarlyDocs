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
		public static IMarkdownInSection DotNetCommentGroupToMarkdown(DotNetCommentGroup group, DotNetMember parent = null)
		{
			if(group.Tag == CommentTag.See || group.Tag == CommentTag.SeeAlso)
			{
				MarkdownLine line = new MarkdownLine();
				foreach(DotNetComment comment in group.Comments)
				{
					line.Concat(DotNetCommentsToLine(comment, parent));
				}
				return line;
			}
			else
			{
				MarkdownParagraph paragraph = new MarkdownParagraph();
				foreach(DotNetComment comment in group.Comments)
				{
					DotNetCommentsFillParagraph(paragraph, comment, parent);
				}
				return paragraph;
			}
		}

		/// <summary>
		/// Returns the first line of comments produced by the group.
		/// Intended for groups that boil down to a single line.
		/// </summary>
		public static MarkdownLine DotNetCommentGroupToMarkdownLine(DotNetCommentGroup group, DotNetMember parent = null)
		{
			MarkdownLine line = new MarkdownLine();
			foreach(DotNetComment comment in group.Comments)
			{
				line.Concat(DotNetCommentsToLine(comment, parent));
			}
			return line;
		}

		public static void DotNetCommentsFillParagraph(MarkdownParagraph paragraph, DotNetComment comment, DotNetMember parent = null)
		{
			if(comment is DotNetCommentQualifiedLinkedGroup && (comment.Tag == CommentTag.See || comment.Tag == CommentTag.SeeAlso))
			{
				paragraph.Add(ToMDLink(comment as DotNetCommentQualifiedLinkedGroup, parent));
			}
			else if(comment is DotNetCommentGroup)
			{
				paragraph.Add(DotNetCommentGroupToMarkdown(comment as DotNetCommentGroup, parent));
			}
			else if(comment is DotNetCommentCodeBlock)
			{
				paragraph.Add(new MarkdownCodeBlock((comment as DotNetCommentCodeBlock).Text, (comment as DotNetCommentCodeBlock).Language));
			}
			else if(comment is DotNetCommentCode)
			{
				paragraph.Add(new MarkdownCode((comment as DotNetCommentCode).Text));
			}
			else if(comment is DotNetCommentText)
			{
				string text = (comment as DotNetCommentText).Text;
				if(text.Contains("\n"))
				{
					if(text.EndsWith("\n"))
					{
						text = text.Substring(0, text.Length - 1);
					}
					paragraph.Add(text.Split('\n').Select(t => new MarkdownLine(t)).ToArray());
				}
				else
					paragraph.Add(new MarkdownText(text));
			}
			else if(comment is DotNetCommentList)
			{
				paragraph.Add(ToMDList(comment as DotNetCommentList));
			}
			else if(comment is DotNetCommentTable)
			{
				paragraph.Add(ToMDTable(comment as DotNetCommentTable));
			}
			else if(comment is DotNetCommentQualifiedLink)
			{
				paragraph.Add(ToMDLink(comment as DotNetCommentQualifiedLink, parent));
			}
		}

		public static MarkdownParagraph DotNetCommentsToParagraph(DotNetComment comment, DotNetMember parent = null)
		{
			MarkdownParagraph paragraph = new MarkdownParagraph();
			DotNetCommentsFillParagraph(paragraph, comment, parent);
			return paragraph;
		}

		public static MarkdownLine DotNetCommentsToLine(DotNetComment comment, DotNetMember parent = null)
		{
			MarkdownLine line = new MarkdownLine();

			if(comment is DotNetCommentQualifiedLinkedGroup && (comment.Tag == CommentTag.See || comment.Tag == CommentTag.SeeAlso))
			{
				line.Add(ToMDLink(comment as DotNetCommentQualifiedLinkedGroup, parent));
			}
			else if(comment is DotNetCommentCode)
			{
				line.Add(new MarkdownCode((comment as DotNetCommentCode).Text));
			}
			else if(comment is DotNetCommentText)
			{
				string text = (comment as DotNetCommentText).Text;
				line.Add(new MarkdownText(text));
			}
			else if(comment is DotNetCommentQualifiedLink)
			{
				line.Add(ToMDLink(comment as DotNetCommentQualifiedLink, parent));
			}

			return line;
		}

		public static MarkdownList ToMDList(DotNetCommentList commentList)
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

		public static MarkdownTable ToMDTable(DotNetCommentTable commentTable)
		{
			MarkdownTable markdownTable = new MarkdownTable();

			foreach(DotNetCommentRow commentRow in commentTable.Rows)
			{
				MarkdownTableRow markdownRow = ToMDTableRow(commentRow);
				markdownTable.Add(markdownRow);
			}

			return markdownTable;
		}

		public static MarkdownTableRow ToMDTableRow(DotNetCommentRow commentRow)
		{
			return new MarkdownTableRow(commentRow.Cells.Select(c => c.Text).ToArray());
		}

		public static MarkdownList EnumToMinimalMDList(DotNetType _enum)
		{
			MarkdownList list = new MarkdownList(isNumbered: false);

			foreach(DotNetField field in _enum.Fields)
			{
				list.Add(new MarkdownLine(field.ConstantValue + ": " + field.Name.LocalName));
			}

			return list;
		}

		public static IMarkdownInLine ToMDLink(DotNetCommentQualifiedLink commentLink, DotNetMember parent = null)
		{
			string _namespace = null;
			if(parent != null)
			{
				if(parent is DotNetType)
					_namespace = parent.Name.FullName;
				else _namespace = parent.Name.FullNamespace;
			}
			string text = commentLink.Name.ToDisplayString(_namespace);
			string url = commentLink.Name.ToStringLink();

			if(url.Contains("http"))
				return new MarkdownInlineLink(text, url);

			if(url.EndsWith(Ext.MD))
			{
				if(parent == null || parent.Name != commentLink.Name.FullNamespace)
					return new MarkdownInlineLink(text, url);
			}

			return new MarkdownText(text);
		}

		public static IMarkdownInLine ToMDLink(DotNetCommentQualifiedLinkedGroup commentGroup, DotNetMember parent = null)
		{
			string text = String.Join("", DotNetCommentGroupToMarkdownLine(commentGroup, parent).Elements.Select(x => x.ToMarkdown(null)).ToArray());
			IMarkdownInLine plainLink = ToMDLink(commentGroup.QualifiedLink, parent);
			if(plainLink is MarkdownInlineLink)
			{
				return new MarkdownInlineLink(text, (plainLink as MarkdownInlineLink).Url);
			}
			return new MarkdownText(text);
		}

	}
}
