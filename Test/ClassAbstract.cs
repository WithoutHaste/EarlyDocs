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
