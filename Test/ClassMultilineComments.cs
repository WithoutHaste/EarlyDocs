using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	/// <summary>
	/// Tests different ways to format multiline comments.
	/// </summary>
	public class ClassMultilineComments
	{
		/// <summary>
		/// [Summary Tag] [Multiline with Endline]
		/// Etiam enim odio, imperdiet finibus fringilla 
		/// ut, ultrices id tortor. Vivamus finibus feugiat 
		/// purus lacinia venenatis. Vestibulum nec augue 
		/// nunc. Fusce euismod purus vel nibh euismod rutrum. 
		/// Ut vel arcu sed libero ultrices bibendum ac ut 
		/// purus. Phasellus elit nisl, posuere eu ullamcorper 
		/// a, vestibulum a odio. Nulla id odio egestas, 
		/// volutpat ligula ut, laoreet sem. 
		/// </summary>
		public int FieldA = 0;

		/// <summary>
		/// <para>[Summary Tag] [Multiline with Paragraphs]</para>
		/// <para>Nunc tellus justo, venenatis eu ornare vel, laoreet in nulla. Nam id ullamcorper ipsum, vitae iaculis diam. Aliquam ullamcorper quam at ex egestas facilisis. Duis condimentum tellus dui, at porttitor dolor efficitur ac.</para>
		/// <para>Quisque arcu risus, imperdiet aliquam posuere sed, viverra nec sem. In vitae metus quis urna interdum elementum pulvinar vel quam. Quisque dolor nisl, laoreet nec vulputate nec, lacinia vel ex. Ut dui sem, scelerisque quis accumsan non, pharetra sed ante. Etiam at mi nulla.</para>
		/// <para>Etiam mauris arcu, cursus ac nisi ut, porttitor maximus libero. Mauris at laoreet justo, in efficitur justo. In eleifend imperdiet risus, eu viverra metus maximus a. Nunc dapibus, dui et accumsan tristique, nulla augue pharetra arcu, eget convallis metus leo consectetur justo. Cras urna mi, efficitur at aliquet eu, rutrum imperdiet est.</para>
		/// </summary>
		public int FieldB = 0;
	}
}
