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
	public class ChildException : Exception
	{
		/// <summary></summary>
		public ChildException(string message) : base(message)
		{
		}
	}
}
