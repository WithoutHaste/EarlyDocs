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
		/// [Summary Tag] [Short] [One Line] Proin non lectus in ante egestas porta.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Sed eu tortor magna.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Quisque volutpat hendrerit ex, sit amet accumsan ligula tincidunt et.</example>
		/// <permission cref="FieldNotAbstract">[Permission Tag] [References Field] [Short] [One Line] Mauris tempor sollicitudin metus, a tristique ex fermentum eget.</permission>
		/// [Floating Comment] [Short] [One Line] Cras ac aliquam risus, ut sagittis dolor.
		public int FieldNotAbstract { get; set; }

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Sed pharetra elit eget felis iaculis, nec mattis urna malesuada.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Morbi at est eros.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Sed cursus mauris nec lorem dapibus, ut bibendum sem hendrerit.</example>
		/// <permission cref="PropertyAbstract">[Permission Tag] [References Property] [Short] [One Line] Integer metus lacus, faucibus eget neque nec, congue sodales mauris.</permission>
		/// [Floating Comment] [Short] [One Line] Cras malesuada non ligula bibendum cursus.
		public abstract int PropertyAbstract { get; set; }

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Ut quam purus, laoreet ac tortor eget, lobortis finibus velit.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Etiam accumsan dignissim odio a suscipit.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Aliquam erat volutpat.</example>
		/// <permission cref="PropertyNotAbstract">[Permission Tag] [References Property] [Short] [One Line] Fusce vestibulum velit ac dignissim hendrerit.</permission>
		/// [Floating Comment] [Short] [One Line] Integer volutpat libero vitae turpis ornare blandit.
		public int PropertyNotAbstract { get; set; }

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
		/// [Summary Tag] [Short] [One Line] Suspendisse libero ex, pretium vel quam eget, convallis faucibus tortor.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Nunc lorem magna, hendrerit eu justo vitae, iaculis consequat odio.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Phasellus consectetur finibus mi quis dictum.</example>
		/// <permission cref="this[string]">[Permission Tag] [References Indexer] [Short] [One Line] Fusce eu auctor arcu, ut interdum neque.</permission>
		/// [Floating Comment] [Short] [One Line] Integer elementum mauris ut venenatis consectetur.
		public string this[string key] { get { return "0"; } }

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

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Suspendisse luctus scelerisque ipsum vitae cursus.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Integer vestibulum elit sit amet diam bibendum ultrices.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Vestibulum ultricies metus ante, a eleifend quam maximus in.</example>
		/// <permission cref="MethodNotAbstract()">[Permission Tag] [References Method] [Short] [One Line] Sed molestie, sapien non lobortis cursus, elit neque rhoncus nunc, sit amet pellentesque ex mauris vel velit.</permission>
		/// <exception cref="FileNotFoundException">[Exception Tag] [Short] [One Line] Donec elit turpis, semper faucibus ligula et, ullamcorper lacinia odio.</exception>
		/// <returns>[Returns Tag] [Short] [One Line] In vestibulum imperdiet ligula, ac malesuada metus sodales quis.</returns>
		/// [Floating Comment] [Short] [One Line] Proin quis justo id dolor posuere consequat non non nulla.
		public double MethodNotAbstract()
		{
			return 0;
		}
	}
}
