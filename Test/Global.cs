using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	/// <summary>
	/// Tests an enum declared in a namespace with simple comments.
	/// </summary>
	public enum EnumGlobalSimple
	{
		/// <summary>[Summary Tag] [Short] [One Line] Suspendisse neque diam, varius sit amet leo et, dapibus sagittis ipsum.</summary>
		Invalid = 0,
		/// <summary>[Summary Tag] [Short] [One Line] Ut rutrum augue sed cursus varius.</summary>
		Aliquam,
		/// <summary>[Summary Tag] [Short] [One Line] Nam efficitur velit tellus, sit amet venenatis orci gravida sed.</summary>
		Suscipit,
		/// <summary>[Summary Tag] [Short] [One Line] Mauris vel ante facilisis, tincidunt nisl nec, porta sapien.</summary>
		Efficitur
	};

	/// <summary>
	/// Tests an enum declared in a namespace with complex comments
	/// </summary>
	public enum EnumGlobalComplex
	{
		//todo
	};

	/// <summary></summary>
	internal enum EnumGlobalInternal
	{
		Invalid = 0
	};
}
