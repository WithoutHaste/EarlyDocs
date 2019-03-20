using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// Tests class inheritance.
	/// </summary>
	public class ClassInherits : ClassSimpleComments, IInterfaceSimpleComments, Filler.IInterfaceB
	{
		/// <inheritdoc/>
		public int PropertyA { get; set; }

		/// <permission cref='P:Test.ClassInherits.Test#IInterfaceSimpleComments#PropertyB'>[Permission Tag] [Explcit Interface Implementation Property]</permission>
		int IInterfaceSimpleComments.PropertyB { get; set; }

		/// <inheritdoc/>
		int Filler.IInterfaceB.PropertyB { get; set; }

		/// <inheritdoc/>
		public void MethodA()
		{
		}

		/// <permission cref='M:Test.ClassInherits.Test#IInterfaceSimpleComments#MethodB'>[Permission Tag] [Explcit Interface Implementation Method]</permission>
		void IInterfaceSimpleComments.MethodB()
		{
		}

		/// <inheritdoc/>
		void Filler.IInterfaceB.MethodB()
		{
		}

		/// <inheritdoc/>
		public override int MethodVirtual()
		{
			return 1;
		}
	}
}
