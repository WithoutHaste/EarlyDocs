using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	/// <summary>
	/// Tests abstract class.
	/// </summary>
	public abstract class ClassAbstract
	{
		/// <summary>
		/// [Summary Tag] [Short] [One Line] Sed pharetra elit eget felis iaculis, nec mattis urna malesuada.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Morbi at est eros.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Sed cursus mauris nec lorem dapibus, ut bibendum sem hendrerit.</example>
		/// <permission cref="PropertyA">[Permission Tag] [References Property] [Short] [One Line] Integer metus lacus, faucibus eget neque nec, congue sodales mauris.</permission>
		/// [Floating Comment] [Short] [One Line] Cras malesuada non ligula bibendum cursus.
		public abstract int PropertyA { get; set; }

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Suspendisse et ultricies justo, et sagittis nibh.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Proin non dui efficitur risus facilisis porta at at lectus.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Maecenas pretium gravida laoreet.</example>
		/// <permission cref="this[int]">[Permission Tag] [References Indexer] [Short] [One Line] Etiam convallis non elit ac dictum.</permission>
		/// [Floating Comment] [Short] [One Line] Vestibulum finibus nulla nec dui facilisis, sed venenatis sem dictum.
		public abstract int this[int key] { get; set; }

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Donec non commodo ante, at commodo nulla.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Aliquam placerat nisi euismod eleifend efficitur.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Aliquam auctor est sed condimentum vulputate.</example>
		/// <permission cref="EventA">[Permission Tag] [References Event] [Short] [One Line] Donec non metus sed felis interdum ullamcorper non non dolor.</permission>
		/// [Floating Comment] [Short] [One Line] Sed at ipsum at tortor congue elementum.
		public abstract event EventHandler EventA;

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Donec facilisis, nibh eu faucibus rhoncus, diam ipsum scelerisque magna, imperdiet aliquet mi erat ut libero.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Quisque dapibus sem eget eros pretium, ut porttitor nibh tempus.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Duis sed quam rutrum, hendrerit est ac, posuere lorem.</example>
		/// <permission cref="MethodAbstract()">[Permission Tag] [References Method] [Short] [One Line] In id facilisis libero.</permission>
		/// <exception cref="FileNotFoundException">[Exception Tag] [Short] [One Line] Cras sed diam ipsum.</exception>
		/// <returns>[Returns Tag] [Short] [One Line] Nam semper purus tempor lectus sagittis pretium.</returns>
		/// [Floating Comment] [Short] [One Line] Mauris lobortis lorem quis odio rhoncus lobortis.
		public abstract double MethodAbstract();
	}
}
