using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
	/// <summary>
	/// Summary of exception.
	/// </summary>
	public class GrandchildException : ChildException
	{
		/// <summary>
		/// Summary of field.
		/// </summary>
		public readonly int Value;

		/// <summary></summary>
		public GrandchildException(string message, int value) : base(message)
		{
			Value = value;
		}
	}
}
