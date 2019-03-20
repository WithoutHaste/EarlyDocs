using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// Tests generic exceptions.
	/// </summary>
	public class ClassExceptionGeneric<Apple> : Exception
	{
		/// <summary></summary>
		public Apple Value { get; protected set; }

		/// <summary></summary>
		public ClassExceptionGeneric(Apple value)
		{
			Value = value;
		}

		/// <summary></summary>
		public Apple MethodA(Apple value)
		{
			return default(Apple);
		}
	}
}
