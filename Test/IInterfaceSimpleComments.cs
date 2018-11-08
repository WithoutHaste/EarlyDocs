using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	/// <summary>
	/// [Summary Tag] [Short] [One Line] Cras posuere mattis nisi, sed porta libero auctor eu.
	/// </summary>
	/// <remarks>
	/// [Remarks Tag] [Long] [One Line] Ut in sem eu purus facilisis venenatis eu id magna.
	/// </remarks>
	/// <example>[Example Tag] [Short] [One Line] Nam eget velit vestibulum, euismod arcu eu, pretium odio.</example>
	/// <permission cref="IInterfaceSimpleComments">[Permission Tag] [References Interface] [Short] [One Line] Cras vel tortor ut massa efficitur sollicitudin.</permission>
	/// [Floating Comment] [Short] [One Line] Donec venenatis vitae elit at faucibus.
	public interface IInterfaceSimpleComments
	{
		/// <summary>
		/// [Summary Tag] [Short] [One Line] Donec non interdum elit, a tincidunt massa.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Long] [One Line] Vivamus at turpis vestibulum, ultricies nibh at, finibus tortor.
		/// </remarks>
		/// <value>
		/// [Value Tag] [Short] [One Line] Etiam vel quam vehicula, semper nulla vel, condimentum augue.
		/// </value>
		/// <example>[Example Tag] [Short] [One Line] Nulla ullamcorper est nec ipsum vehicula, eget tincidunt ante accumsan.</example>
		/// <permission cref="PropertyA">[Permission Tag] [References Property] [Short] [One Line] Cras id egestas justo, a dapibus enim.</permission>
		/// [Floating Comment] [Short] [One Line] Ut massa nibh, rutrum nec urna in, interdum congue justo.
		int PropertyA { get; set; }

		/// <summary>
		/// SimpleComments: Enable test inheriting from multiple interfaces with the same member names.
		/// </summary>
		int PropertyB { get; set; }

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Pellentesque eros est, aliquet non nulla et, porttitor pharetra ligula.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Nullam in quam vel metus faucibus pulvinar.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Vivamus eu mauris cursus, facilisis lectus sit amet, viverra neque.</example>
		/// <permission cref="MethodA()">[Permission Tag] [References Method] [Short] [One Line] Nam justo elit, sagittis ac elementum sed, feugiat id ipsum.</permission>
		/// <exception cref="ArgumentException">[Exception Tag] [Short] [One Line] Donec bibendum mauris finibus magna venenatis rutrum.</exception>
		/// [Floating Comment] [Short] [One Line] Sed vehicula gravida efficitur.
		void MethodA();

		/// <summary>
		/// SimpleComments: Enable test inheriting from multiple interfaces with the same member names.
		/// </summary>
		void MethodB();
	}
}
