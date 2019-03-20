using System;
using System.Collections.Generic;
using System.Text;

namespace Test.Filler
{
	/// <summary>
	/// Enable tests inheriting from multiple interfaces.
	/// </summary>
	public interface IInterfaceB
	{
		/// <summary>
		/// B: Enable test inheriting from multiple interfaces with the same member names.
		/// </summary>
		int PropertyB { get; set; }

		/// <summary>
		/// B: Enable test inheriting from multiple interfaces with the same member names.
		/// </summary>
		void MethodB();
	}
}
