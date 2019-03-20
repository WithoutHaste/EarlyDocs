using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// [Summary Tag] [Short] [One Line] Donec gravida egestas nibh.
	/// </summary>
	/// <remarks>
	/// [Remarks Tag] [Short] [One Line] Maecenas et turpis eget mi dapibus sodales.
	/// </remarks>
	/// <example>[Example Tag] [Short] [One Line] Phasellus id pharetra justo.</example>
	/// <permission cref="EventHandlerGlobal">[Permission Tag] [References Delegate] [Short] [One Line] In commodo, arcu eget vulputate faucibus, justo libero varius nulla, sed tempus nisl nibh in augue.</permission>
	/// <param name="sender">[Param Tag] [Short] [One Line] Nunc bibendum, purus sit amet sodales molestie, libero metus mattis massa, porttitor vehicula sem ligula in quam.</param>
	/// <param name="e">[Param Tag] [Short] [One Line] Integer bibendum, orci ut blandit scelerisque, justo nibh fermentum mi, efficitur malesuada neque velit id ipsum.</param>
	/// [Floating Comment] [Short] [One Line] Quisque neque sapien, vulputate commodo leo non, mollis suscipit nunc.
	public delegate EventHandler EventHandlerGlobal(object sender, EventArgs e);

	/// <summary>
	/// Tests events and delegates.
	/// </summary>
	public class ClassEventsDelegates
	{
		/// <summary>
		/// [Summary Tag] [Short] [One Line] Phasellus ut nulla aliquam, commodo ante ac, accumsan leo.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Duis metus ex, suscipit quis quam vel, viverra imperdiet magna.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Nam vitae risus quis est aliquet egestas.</example>
		/// <permission cref="EventHandlerInClass">[Permission Tag] [References Delegate] [Short] [One Line] Curabitur dolor nisi, porta eget est sed, lobortis venenatis ligula.</permission>
		/// <param name="sender">[Param Tag] [Short] [One Line] NSuspendisse fermentum suscipit purus, sodales dapibus ex faucibus et.</param>
		/// <param name="e">[Param Tag] [Short] [One Line] Vestibulum porttitor consectetur pellentesque.</param>
		/// [Floating Comment] [Short] [One Line] Vivamus ultrices ex sit amet justo aliquet pretium.
		public delegate void EventHandlerInClass(object sender, EventArgs e);

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Mauris sit amet vestibulum justo.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Praesent volutpat nibh vehicula rutrum condimentum.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Nulla placerat ultricies augue, mollis rutrum tellus tempus ut.</example>
		/// <permission cref="EventA">[Permission Tag] [References Event] [Short] [One Line] Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.</permission>
		/// [Floating Comment] [Short] [One Line] Suspendisse sodales sem ut massa gravida aliquet.
		public event EventHandlerGlobal EventA;

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Suspendisse non enim semper sapien volutpat rhoncus.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Fusce vel quam ac urna sagittis suscipit non sit amet nulla.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Vestibulum tincidunt justo ut lorem consectetur, a facilisis tortor lacinia.</example>
		/// <permission cref="EventB">[Permission Tag] [References Event] [Short] [One Line] Quisque egestas ante nec feugiat lacinia.</permission>
		/// [Floating Comment] [Short] [One Line] Nunc faucibus commodo ultrices.
		protected event EventHandlerInClass EventB;

		/// <summary>
		/// [Summary Tag] [Private Member] [Should Ignore] Duis justo dolor, dapibus eu pulvinar vitae, iaculis non neque.
		/// </summary>
		private event EventHandler EventC;

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Suspendisse mattis lacinia orci, eu cursus lacus ultricies ac.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Donec dictum consectetur lacus non sodales.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Aenean mi leo, sagittis a mollis vel, pretium eu justo. In auctor est eget nibh luctus blandit.</example>
		/// <permission cref="ActionInClass">[Permission Tag] [References Action] [Short] [One Line] Quisque egestas ante nec feugiat lacinia.</permission>
		/// [Floating Comment] [Short] [One Line] Donec gravida egestas nibh.
		public Action<int> ActionInClass = new Action<int>(MethodIntVoid);

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Maecenas et turpis eget mi dapibus sodales.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Phasellus id pharetra justo.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] In commodo, arcu eget vulputate faucibus, justo libero varius nulla, sed tempus nisl nibh in augue.</example>
		/// <permission cref="FuncInClass">[Permission Tag] [References Func] [Short] [One Line] Quisque neque sapien, vulputate commodo leo non, mollis suscipit nunc.</permission>
		/// [Floating Comment] [Short] [One Line] Nunc bibendum, purus sit amet sodales molestie, libero metus mattis massa, porttitor vehicula sem ligula in quam.
		public Func<int,string> FuncInClass = new Func<int,string>(MethodIntString);

		public static void MethodIntVoid(int a)
		{
		}

		public static string MethodIntString(int a)
		{
			return "0";
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Integer bibendum, orci ut blandit scelerisque, justo nibh fermentum mi, efficitur malesuada neque velit id ipsum.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Phasellus ut nulla aliquam, commodo ante ac, accumsan leo.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Duis metus ex, suscipit quis quam vel, viverra imperdiet magna.</example>
		/// <permission cref="MethodWithAction(Action{int})">[Permission Tag] [References Method] [Short] [One Line] Nam vitae risus quis est aliquet egestas.</permission>
		/// <exception cref="ArgumentException">[Exception Tag] [Short] [One Line] Curabitur dolor nisi, porta eget est sed, lobortis venenatis ligula.</exception>
		/// <param name="action">[Parameter Tag] [Short] [One Line] Suspendisse fermentum suscipit purus, sodales dapibus ex faucibus et.</param>
		/// [Floating Comment] [Short] [One Line] Vestibulum porttitor consectetur pellentesque.
		public void MethodWithAction(Action<int> action)
		{
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Donec accumsan lacus a dolor auctor, sit amet cursus eros faucibus.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Pellentesque est nibh, vehicula at velit id, scelerisque pretium tortor.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Vivamus lobortis neque eget mi semper finibus.</example>
		/// <permission cref="MethodWithFunc(Func{int,string})">[Permission Tag] [References Method] [Short] [One Line] In aliquet libero in euismod rutrum.</permission>
		/// <exception cref="ArgumentException">[Exception Tag] [Short] [One Line] Sed fermentum ornare nisi, et vestibulum lorem tincidunt in.</exception>
		/// <param name="func">[Parameter Tag] [Short] [One Line] Aenean quis mi id leo scelerisque euismod eget vel lacus.</param>
		/// [Floating Comment] [Short] [One Line] In a sodales tortor, quis mollis augue.
		public void MethodWithFunc(Func<int,string> func)
		{
		}

	}
}
