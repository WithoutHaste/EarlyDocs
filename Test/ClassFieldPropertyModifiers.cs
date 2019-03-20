using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// Tests modifiers on fields and properties.
	/// </summary>
	public class ClassFieldPropertyModifiers
	{
		/// <summary>Pellentesque ullamcorper pretium diam eget commodo.</summary>
		public static int StaticField = 0;

		/// <summary>Maecenas scelerisque purus in lacus sollicitudin, eu commodo lacus ultricies.</summary>
		public const int ConstField = 0;

		/// <summary>In sagittis tempor libero eget finibus.</summary>
		public readonly int ReadonlyField = 0;

		/// <summary>Proin et facilisis tellus.</summary>
		public int PublicField = 0;

		/// <summary>Nulla facilisi.</summary>
		internal int InternalField = 0;

		/// <summary>Aenean ac rhoncus nulla, a rutrum turpis.</summary>
		protected int ProtectedField = 0;

		/// <summary>Morbi neque leo, posuere id nulla in, laoreet fermentum dolor.</summary>
		protected internal int ProtectedInternalField = 0;

		/// <summary>Donec suscipit dolor rhoncus urna blandit tempus.</summary>
		private int PrivateField = 0;

		/// <summary>Sed consectetur tincidunt velit at suscipit.</summary>
		public int PublicPublicProperty { get; set; }

		/// <summary>Nam urna purus, aliquam a metus quis, cursus interdum ligula.</summary>
		public int PublicProtectedProperty { get; protected set; }

		/// <summary>Aliquam in sagittis ipsum.</summary>
		public int PublicPrivateProperty { get; private set; }

		/// <summary>Mauris et tincidunt ligula, nec efficitur justo.</summary>
		protected int ProtectedProtectedProperty { get; set; }

		/// <summary>Quisque sapien lacus, convallis eget mi at, tristique elementum nibh.</summary>
		protected int GetOnlyProperty { get; }

		/// <summary>Vestibulum dapibus porttitor eros, eget elementum lectus mattis sit amet.</summary>
		protected int SetOnlyProperty { set { } }
	}
}
