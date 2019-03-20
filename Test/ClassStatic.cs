using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// Tests static class.
	/// </summary>
	public static class ClassStatic
	{
		/// <summary>
		/// Test return types on static methods.
		/// </summary>
		public static string MethodA()
		{
			return "0";
		}

#if EXTENSION_METHODS
		/// <summary>
		/// Test extension method parameters.
		/// </summary>
		/// <param name="a">Words a</param>
		/// <param name="b">Words b</param>
		public static void MethodExtension(this string a, string b)
		{
		}
#endif
	}
}
