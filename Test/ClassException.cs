using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// Supports test to link to custom exception.
	/// </summary>
	public class ClassException : Exception
	{
		/// <summary></summary>
		public ClassException(string message) : base(message)
		{
		}
	}
}
