using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// Tests formatting errors in xml comments.
	/// </summary>
	public class ClassFormattingErrors
	{
		/// <permission>[Permission Tag] [No Cref Attribute]</permission>
		/// <exception>[Exception Tag] [No Cref Attribute]</exception>
		/// <duplicate>[Duplicate Tag] [No Cref Attribute]</duplicate>
		/// <see>[See Tag] [No Cref Attribute]</see>
		/// <seealso>[SeeAlso Tag] [No Cref Attribute]</seealso>
		public int CrefMissing = 0;

		/// <param>[Param Tag] [No Name Attribute]</param>
		/// <typeparam>[TypeParam Tag] [No Name Attribute]</typeparam>
		/// <paramref>[ParamRef Tag] [No Name Attribute]</paramref>
		/// <typeparamref>[TypeParamRef Tag] [No Name Attribute]</typeparamref>
		public int NameMissing = 0;
	}
}
