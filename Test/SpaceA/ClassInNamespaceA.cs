using System;
using System.Collections.Generic;
using System.Text;

namespace Test.SpaceA
{
	/// <summary>
	/// Tests a class in a nested namespace.
	/// </summary>
	public class ClassInNamespaceA
	{
		/// <summary></summary>
		public int FieldA = 0;

		/// <summary></summary>
		public ClassInNamespaceA()
		{
		}

		/// <summary>
		/// Tests a class nested in a class.
		/// </summary>
		public class ClassInClass
		{
			/// <summary></summary>
			public int FieldA = 0;

			/// <summary></summary>
			public ClassInClass()
			{
			}
		}
	}
}
