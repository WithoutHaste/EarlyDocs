using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	/// <summary>
	/// Tests class inheritance.
	/// </summary>
	public class ClassInherits : ClassSimpleComments, IInterfaceSimpleComments
	{
		/// <inheritdoc/>
		public int PropertyA { get; set; }

		/// <inheritdoc/>
		public void MethodA()
		{
		}

		/// <inheritdoc/>
		public override int MethodVirtual()
		{
			return 1;
		}
	}
}
