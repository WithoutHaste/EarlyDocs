using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
