using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// Tests struct inheritance.
	/// </summary>
	public struct StructInherits : IInterfaceSimpleComments
	{
		/// <summary>
		/// [Summary Tag] [Does Not Inherit]
		/// </summary>
		public int PropertyA { get; set; }

		/// <inheritdoc/>
		public int PropertyB { get; set; }

		/// <summary>
		/// [Summary Tag] [Does Not Inherit]
		/// </summary>
		public void MethodA() { }

		/// <inheritdoc/>
		public void MethodB() { }
	}
}
