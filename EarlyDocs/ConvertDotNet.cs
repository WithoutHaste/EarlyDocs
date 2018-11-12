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
		public static MarkdownParagraph DotNetCommentGroupToMarkdown(DotNetCommentGroup group, DotNetMember parent = null)
		{
			return DotNetCommentsToMarkdown(group.Comments, parent);
		}

		/// <summary>
		/// Returns the first line of comments produced by the group.
		/// Intended for groups that boil down to a single line.
		/// </summary>
		public static MarkdownLine DotNetCommentGroupToMarkdownLine(DotNetCommentGroup group, DotNetMember parent = null)
		{
			MarkdownParagraph paragraph = DotNetCommentGroupToMarkdown(group, parent);
			while(paragraph.Elements.Length > 0 && paragraph.Elements[0] is MarkdownParagraph)
				paragraph = (paragraph.Elements[0] as MarkdownParagraph);

			MarkdownLine line = new MarkdownLine();

			foreach(IMarkdownInSection inSection in paragraph.Elements)
			{
				if(inSection is MarkdownLine)
				{
					line.Concat(inSection as MarkdownLine);
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

		public static MarkdownParagraph DotNetCommentsToMarkdown(List<DotNetComment> list, DotNetMember parent = null)
		{
			MarkdownParagraph paragraph = new MarkdownParagraph();

			foreach(DotNetComment comment in list)
			{
				paragraph.Add(DotNetCommentsToMarkdown(comment, parent));
			}

			return paragraph;
		}


		//todo: refactor: extend the types instead of using if/else trees
		public static MarkdownParagraph DotNetCommentsToMarkdown(DotNetComment comment, DotNetMember parent = null)
		{
			MarkdownParagraph paragraph = new MarkdownParagraph();

			if(comment is DotNetCommentQualifiedLinkedGroup && (comment.Tag == CommentTag.See || comment.Tag == CommentTag.SeeAlso))
			{
				paragraph.Add(DotNetCommentsToMarkdown(comment as DotNetCommentQualifiedLinkedGroup, parent));
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
				paragraph.Add(DotNetCommentsToMarkdown(comment as DotNetCommentList));
			}
			else if(comment is DotNetCommentTable)
			{
				paragraph.Add(DotNetCommentsToMarkdown(comment as DotNetCommentTable));
			}
			else if(comment is DotNetCommentQualifiedLink)
			{
				paragraph.Add(DotNetCommentsToMarkdown(comment as DotNetCommentQualifiedLink, parent));
			}

			return paragraph;
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

		public static IMarkdownInLine DotNetCommentsToMarkdown(DotNetCommentQualifiedLink commentLink, DotNetMember parent = null)
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

		public static IMarkdownInLine DotNetCommentsToMarkdown(DotNetCommentQualifiedLinkedGroup commentGroup, DotNetMember parent = null)
		{
			string text = String.Join("", DotNetCommentGroupToMarkdownLine(commentGroup, parent).Elements.Select(x => x.ToMarkdown(null)).ToArray());
			IMarkdownInLine plainLink = DotNetCommentsToMarkdown(commentGroup.QualifiedLink, parent);
			if(plainLink is MarkdownInlineLink)
			{
				return new MarkdownInlineLink(text, (plainLink as MarkdownInlineLink).Url);
			}
			return new MarkdownText(text);
		}

	}
}
