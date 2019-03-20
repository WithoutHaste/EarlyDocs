using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// Tests paramref and typeparamref tags.
	/// </summary>
	public class ClassRef
	{
		/// <summary>
		/// The parameter names are: <paramref name="a"/>, <paramref name="b"/>, and <paramref name="c"/>.
		/// </summary>
		/// <param name="a">words a</param>
		/// <param name="b">words b</param>
		/// <param name="c">words c</param>
		public void MethodA(int a, string b, List<int> c)
		{
		}

		/// <summary>
		/// The parameter names are: <paramref name="a"/>, <paramref name="b"/>, and <paramref name="c"/>.
		/// The type-parameter names are: <typeparamref name="A"/>, <typeparamref name="B"/>, and <typeparamref name="C"/>.
		/// </summary>
		/// <typeparam name="A">WORDS A</typeparam>
		/// <typeparam name="B">WORDS B</typeparam>
		/// <typeparam name="C">WORDS C</typeparam>
		/// <param name="a">words a</param>
		/// <param name="b">words b</param>
		/// <param name="c">words c</param>
		public void MethodB<A, B, C>(A a, B b, C c)
		{
		}
	}
}
