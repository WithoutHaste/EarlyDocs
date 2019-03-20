using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// Tests method parameter options.
	/// </summary>
	public class ClassMethodParameters
	{
		/// <summary>
		/// [Summary Tag] Spirius livid fii.
		/// </summary>
		/// <param name="a">[Param Tag] Findus luo.</param>
		/// <param name="b">[Param Tag] Placto vindicto.</param>
		public void MethodOut(int a, out string b)
		{
			b = "0";
		}

		/// <summary>
		/// [Summary Tag] Dimmus rasa ad en.
		/// </summary>
		/// <param name="a">[Param Tag] Sliphus ad nomino.</param>
		/// <param name="b">[Param Tag] Expo quis eredi.</param>
		public void MethodRef(int a, ref string b)
		{
		}

		/// <summary>
		/// [Summary Tag] Flobotum ignus veritay.
		/// </summary>
		/// <param name="a">[Param Tag] Sigus si ficcundus.</param>
		/// <param name="b">[Param Tag] En et tu.</param>
		/// <param name="c">[Param Tag] Cloisen iplorum diggitallus.</param>
		/// <param name="d">[Param Tag] Pinget ob flouritis regus ergo sum.</param>
		public void MethodOptional(int a, string b, double c = 0, object d = null)
		{
		}
	}
}
