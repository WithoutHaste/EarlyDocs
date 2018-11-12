﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
	}
}
