﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WithoutHaste.DataFiles.DotNet;
using WithoutHaste.DataFiles.Markdown;

namespace EarlyDocs
{
	internal static class ConvertDotNet
	{
		internal static IMarkdownInSection DotNetCommentGroupToMarkdown(DotNetCommentGroup group, DotNetMember parent = null)
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
		internal static MarkdownLine DotNetCommentGroupToMarkdownLine(DotNetCommentGroup group, DotNetMember parent = null)
		{
			MarkdownLine line = new MarkdownLine();
			if(group == null)
				return line;

			foreach(DotNetComment comment in group.Comments)
			{
				line.Concat(DotNetCommentsToLine(comment, parent));
			}
			return line;
		}

		internal static void DotNetCommentsFillParagraph(MarkdownParagraph paragraph, DotNetComment comment, DotNetMember parent = null)
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
				if(String.IsNullOrEmpty(text))
					return;
				string[] lines = text.Split('\n');
				if(lines.Length == 0)
					return;
				for(int i = 0; i < lines.Length - 1; i++)
				{
					paragraph.Add(new MarkdownLine(lines[i]));
				}
				if(!String.IsNullOrEmpty(lines.Last()))
				{
					paragraph.Add(new MarkdownText(lines.Last()));
				}
			}
			else if(comment is DotNetCommentList)
			{
				paragraph.Add(ToMDList(comment as DotNetCommentList, parent));
			}
			else if(comment is DotNetCommentTable)
			{
				paragraph.Add(ToMDTable(comment as DotNetCommentTable));
			}
			else if(comment is DotNetCommentQualifiedLink)
			{
				paragraph.Add(ToMDLink(comment as DotNetCommentQualifiedLink, parent));
			}
			else if(comment is DotNetCommentParameterLink) //paramref and typeparamref
			{
				paragraph.Add(ToMDText(comment as DotNetCommentParameterLink));
			}
		}

		internal static MarkdownParagraph DotNetCommentsToParagraph(DotNetComment comment, DotNetMember parent = null)
		{
			MarkdownParagraph paragraph = new MarkdownParagraph();
			DotNetCommentsFillParagraph(paragraph, comment, parent);
			return paragraph;
		}

		internal static MarkdownLine DotNetCommentsToLine(DotNetComment comment, DotNetMember parent = null)
		{
			MarkdownLine line = new MarkdownLine();

			if(comment is DotNetCommentQualifiedLinkedGroup && (comment.Tag == CommentTag.See || comment.Tag == CommentTag.SeeAlso))
			{
				line.Add(ToMDLink(comment as DotNetCommentQualifiedLinkedGroup, parent));
			}
			else if(comment is DotNetCommentText)
			{
				if(comment is DotNetCommentCodeBlock)
				{
					//no action
				}
				else if(comment is DotNetCommentCode)
				{
					line.Add(new MarkdownCode((comment as DotNetCommentCode).Text));
				}
				else
				{
					string text = (comment as DotNetCommentText).Text;
					line.Add(new MarkdownText(text));
				}
			}
			else if(comment is DotNetCommentQualifiedLink)
			{
				line.Add(ToMDLink(comment as DotNetCommentQualifiedLink, parent));
			}
			else if(comment is DotNetCommentParameterLink) //paramref or typeparamref
			{
				line.Add(ToMDText(comment as DotNetCommentParameterLink));
			}

			return line;
		}

		internal static MarkdownList ToMDList(DotNetCommentList commentList, DotNetMember parent = null)
		{
			MarkdownList markdownList = new MarkdownList(isNumbered: commentList.IsNumbered);

			foreach(DotNetCommentListItem commentItem in commentList.Items)
			{
				MarkdownLine text = new MarkdownLine();
				if(commentItem.Term == null && commentItem.Description == null)
				{
					markdownList.Add(text);
					continue;
				}

				if(commentItem.Term == null)
				{
					text = DotNetCommentGroupToMarkdownLine(commentItem.Description, parent);
				}
				else if(commentItem.Description == null)
				{
					text = DotNetCommentGroupToMarkdownLine(commentItem.Term, parent);
				}
				else
				{
					text = DotNetCommentGroupToMarkdownLine(commentItem.Term, parent);
					text.Add(": ");
					text.Concat(DotNetCommentGroupToMarkdownLine(commentItem.Description, parent));
				}
				markdownList.Add(text);
			}

			return markdownList;
		}

		internal static MarkdownTable ToMDTable(DotNetCommentTable commentTable)
		{
			MarkdownTable markdownTable = new MarkdownTable();

			foreach(DotNetCommentRow commentRow in commentTable.Rows)
			{
				MarkdownTableRow markdownRow = ToMDTableRow(commentRow);
				markdownTable.Add(markdownRow);
			}

			return markdownTable;
		}

		internal static MarkdownTableRow ToMDTableRow(DotNetCommentRow commentRow)
		{
			return new MarkdownTableRow(commentRow.Cells.Select(c => c.Text).ToArray());
		}

		internal static MarkdownList EnumToMinimalMDList(DotNetType _enum)
		{
			MarkdownList list = new MarkdownList(isNumbered: false);

			foreach(DotNetField field in _enum.Fields)
			{
				list.Add(new MarkdownLine(field.ConstantValue + ": " + field.Name.LocalName));
			}

			return list;
		}

		internal static IMarkdownInLine ToMDLink(DotNetCommentQualifiedLink commentLink, DotNetMember parent = null)
		{
			DotNetQualifiedName _namespace = null;
			if(parent != null)
			{
				_namespace = (parent is DotNetType)  ? parent.Name : parent.Name.FullNamespace;
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

		internal static IMarkdownInLine ToMDLink(DotNetCommentQualifiedLinkedGroup commentGroup, DotNetMember parent = null)
		{
			string text = String.Join("", DotNetCommentGroupToMarkdownLine(commentGroup, parent).Elements.Select(x => x.ToMarkdownString(null)).ToArray());
			IMarkdownInLine plainLink = ToMDLink(commentGroup.QualifiedLink, parent);
			if(plainLink is MarkdownInlineLink)
			{
				return new MarkdownInlineLink(text, (plainLink as MarkdownInlineLink).Url);
			}
			return new MarkdownText(text);
		}

		internal static MarkdownText ToMDText(DotNetCommentParameterLink comment)
		{
			if(comment == null)
				return new MarkdownText("");
			return MarkdownText.Italic(comment.Name);
		}
	}
}
