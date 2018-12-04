using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	/// <summary>
	/// Tests generic exceptions.
	/// </summary>
	public class ClassExceptionGeneric<T> : Exception
	{
		/// <summary></summary>
		public T Value { get; protected set; }

		/// <summary></summary>
		public ClassExceptionGeneric(T value)
		{
			Value = value;
		}
	}
}
