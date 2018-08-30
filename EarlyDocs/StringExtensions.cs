using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarlyDocs
{
	static class StringExtensions
	{
		/// <summary>
		/// Returns the last term in a period-delimited string.
		/// </summary>
		public static string LastTerm(this string text)
		{
			return text.Split('.').Last();
		}

		/// <summary>
		/// Remove outer-most opening and closing tags.
		/// </summary>
		public static string StripOuterTags(this string text)
		{
			text = text.Trim();
			if(text[0] != '<') return text;
			int openingTagEndIndex = text.IndexOf('>');
			text = text.Substring(openingTagEndIndex + 1);
			int endingTagStartIndex = text.LastIndexOf("</");
			text = text.Substring(0, endingTagStartIndex);
			return text.Trim();
		}

		/// <summary>
		/// Character is a whitespace character.
		/// </summary>
		public static bool IsWhitespace(this char c)
		{
			return (c == ' ' || c == '\n' || c == '\r' || c == '\t');
		}
	}
}
