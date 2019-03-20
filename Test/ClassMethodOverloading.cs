using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// Tests overloaded methods.
	/// </summary>
	public class ClassMethodOverloading
	{
		/// <summary>
		/// [Summary Tag] [Short] [One Line] Ut nec diam blandit, porttitor nisi ut, eleifend nulla.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Etiam placerat turpis et felis posuere, id interdum turpis fringilla.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] In sit amet nisi ac ipsum consectetur venenatis.</example>
		/// <permission cref="MethodOverload(int,int)">[Permission Tag] [References Method] [Short] [One Line] Nulla lacus ligula, elementum sed lacus sit amet, mollis varius lectus.</permission>
		/// <exception cref="ArgumentException">[Exception Tag] [Short] [One Line] Vestibulum pulvinar mattis gravida.</exception>
		/// <param name="a">[Parameter Tag] [Short] [One Line] Phasellus bibendum eros vel lectus tempor, sit amet vehicula tortor mattis.</param>
		/// <param name="b">[Parameter Tag] [Short] [One Line] Donec commodo vestibulum turpis, ac imperdiet mi vestibulum nec.</param>
		/// [Floating Comment] [Short] [One Line] Nulla facilisi.
		public void MethodOverload(int a, int b)
		{
		}

		/// <duplicate cref="MethodOverload(int,int)" />
		public void MethodOverload(double a, double b)
		{
		}

		/// <duplicate cref="MethodOverload(int,int)" />
		public void MethodOverload(float a, float b)
		{
		}

		/// <duplicate cref="MethodOverload(int,int)" />
		public void MethodDuplicateOneLevel()
		{
		}

		/// <duplicate cref="MethodDuplicateOneLevel()" />
		public void MethodDuplicateTwoLevels()
		{
		}

		/// <duplicate cref="MethodDuplicateTwoLevels()" />
		public void MethodDuplicateThreeLevels()
		{
		}
	}
}
