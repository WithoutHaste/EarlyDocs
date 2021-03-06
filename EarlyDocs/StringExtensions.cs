﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace EarlyDocs
{
	internal static class StringExtensions
	{
		/// <summary>
		/// Returns the last term in a period-delimited string.
		/// </summary>
		internal static string LastTerm(this string text)
		{
			return text.Split('.').Last();
		}

		/// <summary>
		/// Remove outer-most opening and closing tags.
		/// </summary>
		internal static string StripOuterTags(this string text)
		{
			text = text.Trim();
			if(text[0] != '<') return text;
			int openingTagEndIndex = text.IndexOf('>');
			text = text.Substring(openingTagEndIndex + 1);
			int endingTagStartIndex = text.LastIndexOf("</");
			text = text.Substring(0, endingTagStartIndex);
			return text;
		}

		/// <summary>
		/// Remove outer-most opening and closing CDATA tags.
		/// </summary>
		internal static string StripOuterCDATATags(this string text)
		{
			text = text.Trim();
			if(!text.StartsWith("<![CDATA[")) return text;
			int openingTagEndIndex = text.IndexOf("A[");
			text = text.Substring(openingTagEndIndex + 2);
			int endingTagStartIndex = text.LastIndexOf("]]>");
			text = text.Substring(0, endingTagStartIndex);
			return text;
		}

		/// <summary>
		/// Character is a whitespace character.
		/// </summary>
		internal static bool IsWhitespace(this char c)
		{
			return (c == ' ' || c == '\n' || c == '\r' || c == '\t');
		}

		/// <summary>
		/// True if this path or url ends with some file extension.
		/// Webpage endings (such as .com) are interpreted as file extensions.
		/// </summary>
		internal static bool EndsWithFileExtension(this string text)
		{
			Regex r = new Regex(@"\.\w+$", RegexOptions.IgnoreCase);
			return (r.IsMatch(text));
		}

		/// <summary>
		/// Return all whitespace from beginning of string to the first non-whitespace character.
		/// Returns an empty string if there are not leading whitespaces.
		/// </summary>
		internal static string GetLeadingWhitespace(this string text)
		{
			if(text.Length == 0 || !text[0].IsWhitespace())
				return "";
			Regex r = new Regex(@"^\s+", RegexOptions.IgnoreCase);
			return r.Match(text).Value;
		}
	}
}
