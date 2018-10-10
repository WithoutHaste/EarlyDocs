using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	/// <summary>
	/// [Summary Tag] [Short] [One Line] Donec porttitor mi in odio fermentum lobortis.
	/// </summary>
	/// <remarks>
	/// [Remarks Tag] [Long] [One Line] Fusce ut turpis nec ipsum hendrerit fermentum a in nisl.
	/// </remarks>
	/// <example>[Example Tag] [Short] [One Line] Nunc suscipit ipsum sed nisi interdum euismod.</example>
	/// <permission cref="StructSimpleComments">[Permission Tag] [References Struct] [Short] [One Line] Integer egestas dictum mollis.</permission>
	/// [Floating Comment] [Short] [One Line] Sed sit amet dapibus nibh, non luctus quam.
	public class StructSimpleComments
	{
		/// <summary>
		/// [Summary Tag] [Short] [One Line] Sed at molestie metus.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Long] [One Line] Vivamus at turpis vestibulum, ultricies nibh at, finibus tortor.
		/// </remarks>
		/// <value>
		/// [Value Tag] [Short] [One Line] Mauris mattis nibh eget lacinia elementum.
		/// </value>
		/// <example>[Example Tag] [Short] [One Line] Curabitur ullamcorper a turpis at luctus.</example>
		/// <permission cref="IntegerField">[Permission Tag] [References Field] [Short] [One Line] Vivamus lobortis vehicula hendrerit.</permission>
		/// [Floating Comment] [Short] [One Line] Suspendisse eget placerat tortor.
		public int IntegerField = 0;

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Aliquam scelerisque vestibulum nulla, a aliquam dui mollis ac.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Long] [One Line] Fusce mauris arcu, ullamcorper sed ipsum quis, viverra fermentum enim.
		/// </remarks>
		/// <value>
		/// [Value Tag] [Short] [One Line] Quisque in aliquam nunc.
		/// </value>
		/// <example>[Example Tag] [Short] [One Line] Nulla sit amet nulla et tortor laoreet aliquam.</example>
		/// <permission cref="StringProperty">[Permission Tag] [References Property] [Short] [One Line] Sed neque magna, dignissim eget ullamcorper non, tempor vitae massa.</permission>
		/// [Floating Comment] [Short] [One Line] Nullam et porttitor mauris, eget porta urna.
		public string StringProperty { get; set; }

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Pellentesque quis ex eu odio pretium vehicula.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Nunc ut diam risus.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Suspendisse orci odio, luctus vel ultrices at, gravida eget enim.</example>
		/// <permission cref="StructSimpleComments()">[Permission Tag] [References Static Constructor] [Short] [One Line] Proin nec magna pretium, pretium magna at, condimentum magna.</permission>
		/// <exception cref="Exception">[Exception Tag] [Short] [One Line] Cras rutrum ante et neque cursus blandit.</exception>
		/// [Floating Comment] [Short] [One Line] Proin rutrum consectetur dui at egestas.
		static StructSimpleComments()
		{
			//todo: how to cref a static constructor?
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Nam sollicitudin, tortor sit amet cursus sollicitudin, nunc sem pretium ante, et ultrices lectus nibh sollicitudin arcu.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Mauris condimentum libero nibh, sit amet hendrerit nulla tempus ut.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Nunc vel ullamcorper augue.</example>
		/// <permission cref="StructSimpleComments()">[Permission Tag] [References Constructor] [Short] [One Line] Sed gravida tristique diam quis rutrum.</permission>
		/// <exception cref="Exception">[Exception Tag] [Short] [One Line] Fusce tristique efficitur ligula, vulputate gravida augue suscipit at.</exception>
		/// [Floating Comment] [Short] [One Line] Vivamus tristique aliquet hendrerit.
		public StructSimpleComments()
		{
		}

		//todo static method

		//todo method with return and parameters
	}
}
