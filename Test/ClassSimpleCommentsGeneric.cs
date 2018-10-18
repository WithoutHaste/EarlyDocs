using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test //should be just like ClassSimpleComments, but for a generic type
{
	/// <summary>
	/// [Summary Tag] [Short] [One Line] Cras suscipit et sapien ac tincidunt.
	/// </summary>
	/// <remarks>
	/// [Remarks Tag] [Long] [One Line] Etiam et pretium magna.
	/// </remarks>
	/// <example>[Example Tag] [Short] [One Line] Duis rhoncus libero non nibh efficitur semper.</example>
	/// <permission cref="ClassSimpleCommentsGeneric{T,U}">[Permission Tag] [References Generic Class] [Short] [One Line] Vestibulum at justo quis velit finibus imperdiet.</permission>
	/// <permission cref="ClassSimpleCommentsGeneric{T,U}.TField">[Permission Tag] [References Field] [Short] [One Line] Vestibulum malesuada volutpat tincidunt.</permission>
	/// <typeparam name="T">[Type Parameter Tag] [Short] [One Line] Aenean pellentesque lectus nec urna faucibus, eget sodales diam mollis.</typeparam>
	/// <typeparam name="U">[Type Parameter Tag] [Short] [One Line] Etiam libero justo, maximus in augue vel, tempus sagittis eros.</typeparam>
	/// [Floating Comment] [Short] [One Line] Mauris cursus erat at ligula pretium dignissim.
	public class ClassSimpleCommentsGeneric<T,U> where T:new() where U:new()
	{
		/// <summary>
		/// [Summary Tag] [Short] [One Line] Sed feugiat, velit vel convallis accumsan, magna dui ultrices lorem, nec sollicitudin lorem nisl sit amet justo.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Donec porta metus at neque vehicula, nec placerat ligula tristique.
		/// </remarks>
		/// <value>
		/// [Value Tag] [Short] [One Line] Sed nec nibh sed nisl gravida eleifend.
		/// </value>
		/// <example>[Example Tag] [Short] [One Line] Fusce ut suscipit ante.</example>
		/// <permission cref="TField">[Permission Tag] [References Field] [Short] [One Line] Vivamus eget fermentum ligula.</permission>
		/// [Floating Comment] [Short] [One Line] Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.
		public T TField = new T();

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Phasellus tellus orci, maximus sed suscipit eu, sollicitudin ut ipsum.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Donec sit amet nulla non turpis vulputate tincidunt.
		/// </remarks>
		/// <value>
		/// [Value Tag] [Short] [One Line] Donec vel viverra quam, et lacinia elit.
		/// </value>
		/// <example>[Example Tag] [Short] [One Line] Nam et elit et arcu gravida ultricies eget in lacus.</example>
		/// <permission cref="UProperty">[Permission Tag] [References Property] [Short] [One Line] Etiam sit amet ligula vel ligula feugiat dictum ac vitae eros.</permission>
		/// [Floating Comment] [Short] [One Line] Donec scelerisque fringilla eros, in egestas turpis.
		public U UProperty { get; set; }

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Nulla non metus at dui condimentum semper.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Suspendisse erat dolor, blandit eu elit quis, ultricies consectetur purus.
		/// </remarks>
		/// <value>
		/// [Value Tag] [Short] [One Line] Cras sit amet neque quis lorem porta blandit.
		/// </value>
		/// <example>[Example Tag] [Short] [One Line] Nunc quis odio dui.</example>
		/// <permission cref="this[int]">[Permission Tag] [References Indexer Property] [Short] [One Line] Sed in sapien pretium, eleifend metus eu, posuere leo.</permission>
		/// [Floating Comment] [Short] [One Line] Donec arcu ligula, porttitor in tincidunt in, molestie in metus.
		public T this[int key] { get { return new T(); } set { } }

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Donec sit amet erat ac diam fermentum porttitor vitae et diam.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Morbi maximus quis lectus sit amet consequat.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Mauris quis metus convallis, consequat dui viverra, commodo sapien.</example>
		/// <exception cref="Exception">[Exception Tag] [Short] [One Line] Nunc ornare dignissim erat, id malesuada urna elementum et.</exception>
		/// [Floating Comment] [Short] [One Line] Nam nec rhoncus diam.
		static ClassSimpleCommentsGeneric()
		{
			//todo: how to reference a static constructor in a cref?
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Cras a arcu sem.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Phasellus bibendum mauris eget justo tempor, non imperdiet dolor maximus.</example>
		/// <permission cref="ClassSimpleCommentsGeneric()">[Permission Tag] [References Constructor] [Short] [One Line] Aenean nec sollicitudin metus.</permission>
		/// <exception cref="Exception">[Exception Tag] [Short] [One Line] Proin vel libero aliquam, gravida nisl id, ornare urna.</exception>
		/// [Floating Comment] [Short] [One Line] Ut nunc mauris, varius at enim in, elementum tempor metus.
		public ClassSimpleCommentsGeneric()
		{
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Phasellus interdum purus interdum dolor lacinia mollis.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Duis ligula libero, blandit sit amet ante id, porttitor mattis dui.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] In gravida, ex accumsan pellentesque pretium, elit ex pharetra arcu, feugiat maximus dui est nec ligula.</example>
		/// <permission cref="ClassSimpleCommentsGeneric(T,U)">[Permission Tag] [References Constructor] [Short] [One Line] Pellentesque vitae metus nec sem tristique mollis pharetra ac magna.</permission>
		/// <exception cref="ArgumentException">[Exception Tag] [Short] [One Line] Cras gravida vulputate augue quis blandit.</exception>
		/// <param name="t">[Parameter Tag] [Short] [One Line] Morbi dui risus, fringilla vitae felis non, lobortis fringilla dolor.</param>
		/// <param name="u">[Parameter Tag] [Short] [One Line] Proin ac eros a eros pretium tristique.</param>
		/// [Floating Comment] [Short] [One Line] Duis at enim gravida, commodo purus a, vulputate ligula.
		public ClassSimpleCommentsGeneric(T t, U u)
		{
		}


		/// <summary>
		/// [Summary Tag] [Short] [One Line] Quisque scelerisque elit a massa semper luctus.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Sed at scelerisque nulla, sit amet hendrerit quam.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Praesent ligula enim, mattis et justo vel, tempor ultrices nunc.</example>
		/// <permission cref="Finalize()">[Permission Tag] [References Destructor] [Short] [One Line] Nulla eleifend euismod blandit.</permission>
		/// <exception cref="IndexOutOfRangeException">[Exception Tag] [Short] [One Line] Phasellus id tellus odio.</exception>
		/// [Floating Comment] [Short] [One Line] Suspendisse potenti.
		~ClassSimpleCommentsGeneric()
		{
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Mauris malesuada varius risus eu condimentum.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Cras luctus erat tincidunt dolor finibus, et lobortis mi vulputate.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Fusce scelerisque, sem ut mattis condimentum, arcu ligula egestas mi, eu vulputate turpis ante ut purus.</example>
		/// <permission cref="StaticMethodWithParametersWithReturn(T,string)">[Permission Tag] [References Method] [Short] [One Line] Phasellus sollicitudin, augue non dictum varius, diam urna aliquet odio, ut tincidunt leo sem id leo.</permission>
		/// <exception cref="ArgumentException">[Exception Tag] [Short] [One Line] Duis et quam ac nunc posuere condimentum.</exception>
		/// <param name="t">[Parameter Tag] [Short] [One Line] Donec tristique hendrerit ex, vel semper tellus facilisis eu.</param>
		/// <param name="b">[Parameter Tag] [Short] [One Line] Integer sit amet tempor massa.</param>
		/// <returns>[Returns Tag] [Short] [One Line] Donec non nibh dignissim, imperdiet nibh non, aliquet felis.</returns>
		/// [Floating Comment] [Short] [One Line] Nulla dignissim orci id justo laoreet, non consequat lorem semper.
		public static U StaticMethodWithParametersWithReturn(T t, string b)
		{
			return new U();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Maecenas molestie purus id fermentum dictum.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Vestibulum volutpat congue ligula eget scelerisque.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Vivamus ultrices tristique est ultrices bibendum.</example>
		/// <permission cref="MethodWithTypeParameters{A,B}(A,B,U)">[Permission Tag] [References Method] [Short] [One Line] Etiam ut dolor bibendum, malesuada ligula eu, maximus sapien.</permission>
		/// <exception cref="ArgumentException">[Exception Tag] [Short] [One Line] Nunc purus libero, scelerisque a felis et, feugiat ultrices ipsum.</exception>
		/// <param name="a">[Parameter Tag] [Short] [One Line] Proin pulvinar mattis faucibus.</param>
		/// <param name="b">[Parameter Tag] [Short] [One Line] Integer congue sem lorem, eu porttitor nisi consequat non.</param>
		/// <param name="u">[Parameter Tag] [Short] [One Line] Phasellus sit amet euismod lorem, non maximus sem.</param>
		/// <typeparam name="A">[Type Parameter Tag] [Short] [One Line] Aenean neque ipsum, finibus at dolor nec, hendrerit gravida odio.</typeparam>
		/// <typeparam name="B">[Type Parameter Tag] [Short] [One Line] Nunc faucibus sed elit ut pellentesque.</typeparam>
		/// [Floating Comment] [Short] [One Line] Morbi sagittis ipsum nec lorem volutpat, sit amet sodales nulla faucibus.
		public void MethodWithTypeParameters<A,B>(A a, B b, U u)
		{
		}
	}
}
