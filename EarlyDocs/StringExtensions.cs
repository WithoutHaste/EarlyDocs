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
	}
}
