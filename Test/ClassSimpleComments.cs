using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	/// <summary>
	/// [Summary Tag] [Short] [One Line] Lorem ipsum dolor sit amet, consectetur adipiscing elit.
	/// </summary>
	/// <remarks>
	/// [Remarks Tag] [Long] [One Line] Sed nibh turpis, pulvinar eget odio sed, ornare cursus nunc. Curabitur commodo nibh egestas lectus porttitor placerat. Lorem ipsum dolor sit amet, consectetur adipiscing elit. Quisque malesuada accumsan dolor tempus viverra. Nam finibus est non nisi volutpat pharetra. Suspendisse sollicitudin sed lectus vitae tincidunt. Vivamus finibus dapibus dui, venenatis ullamcorper ex egestas accumsan. Curabitur quis augue erat. Donec convallis faucibus magna accumsan vulputate. Vestibulum in vulputate dui, tristique hendrerit metus. Integer ligula lectus, porta ac turpis eu, fringilla pellentesque nisi.
	/// </remarks>
	/// <example>[Example Tag] [Short] [One Line] Duis rhoncus libero non nibh efficitur semper.</example>
	/// <permission cref="ClassSimpleComments">[Permission Tag] [References Class] [Short] [One Line] Pellentesque ac odio pharetra, blandit turpis sed, ultrices massa.</permission>
	/// <permission cref="ClassSimpleComments.IntegerField">[Permission Tag] [References Field] [Short] [One Line] Quisque aliquam diam at ullamcorper pellentesque.</permission>
	/// [Floating Comment] [Short] [One Line] Pellentesque quis augue vel orci semper mattis.
	public class ClassSimpleComments
    {
		/// <summary>
		/// [Summary Tag] [Short] [One Line] Pellentesque mattis lacus turpis, at elementum tortor eleifend eu.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Long] [One Line] Integer elementum erat nibh, eu placerat massa dignissim vel.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Nulla id placerat quam.</example>
		/// <permission cref="EnumInClassSimple">[Permission Tag] [References Enum] [Short] [One Line] Sed in turpis in mauris vulputate varius ut eu magna.</permission>
		/// [Floating Comment] [Short] [One Line] Suspendisse ornare a lectus ac suscipit.
		public enum EnumInClassSimple
		{
			/// <summary>[Summary Tag] [Short] [One Line] Nulla facilisi.</summary>
			Invalid = 0,
			/// <summary>[Summary Tag] [Short] [One Line] Aliquam egestas magna vel eros aliquet, sed ultricies est faucibus.</summary>
			Aliquam,
			/// <summary>[Summary Tag] [Short] [One Line] Praesent non nibh a velit lobortis commodo.</summary>
			Suscipit,
			/// <summary>[Summary Tag] [Short] [One Line] Vivamus sed dui tristique, auctor augue a, bibendum sem.</summary>
			Efficitur
		};

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Nam ut feugiat urna, eu varius arcu.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Suspendisse at placerat libero.
		/// </remarks>
		/// <value>
		/// [Value Tag] [Short] [One Line] Cras nec ultricies augue, at vulputate purus.
		/// </value>
		/// <example>[Example Tag] [Short] [One Line] Nulla vitae malesuada ante.</example>
		/// <permission cref="IntegerField">[Permission Tag] [References Field] [Short] [One Line] Proin at mi aliquet, maximus sem vitae, hendrerit eros.</permission>
		/// [Floating Comment] [Short] [One Line] Nulla ullamcorper leo vel leo aliquet consequat.
		public int IntegerField = 0;

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Aliquam quis dignissim mi.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Curabitur cursus libero eu tincidunt iaculis.
		/// </remarks>
		/// <value>
		/// [Value Tag] [Short] [One Line] Quisque venenatis risus sit amet tellus eleifend tincidunt.
		/// </value>
		/// <example>[Example Tag] [Short] [One Line] Nunc tincidunt sapien a cursus condimentum.</example>
		/// <permission cref="IntegerProperty">[Permission Tag] [References Property] [Short] [One Line] Aliquam interdum nisi ac aliquet pellentesque.</permission>
		/// [Floating Comment] [Short] [One Line] Phasellus ac lobortis nisi, ac bibendum nulla.
		public int IntegerProperty { get; set; }

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Integer condimentum gravida ante at blandit.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Aliquam luctus libero dui, sit amet porta tellus scelerisque eget.
		/// </remarks>
		/// <value>
		/// [Value Tag] [Short] [One Line] Praesent sem ipsum, posuere a venenatis quis, placerat eu orci.
		/// </value>
		/// <example>[Example Tag] [Short] [One Line] Nullam in sem ac ipsum dignissim gravida.</example>
		/// <permission cref="this[int]">[Permission Tag] [References Indexer Property] [Short] [One Line] Curabitur luctus mi lobortis sem euismod, nec egestas purus feugiat.</permission>
		/// [Floating Comment] [Short] [One Line] Mauris aliquam pharetra ex id posuere.
		public string this[int key] { get { return "0"; } set { } }

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Fusce ac lorem nisi.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Donec ullamcorper eros vitae dolor tempus tristique.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Proin in lacus at dui lacinia rutrum ac sed libero.</example>
		/// <exception cref="Exception">[Exception Tag] [Short] [One Line] Vivamus in lectus facilisis, ornare augue nec, semper dolor.</exception>
		/// [Floating Comment] [Short] [One Line] Nam auctor mollis congue.
		static ClassSimpleComments()
		{
			//todo: how to reference a static constructor in a cref?
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Nam ornare velit eget sagittis hendrerit.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Sed nec tincidunt velit.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Suspendisse ut euismod est, nec dictum augue.</example>
		/// <permission cref="ClassSimpleComments()">[Permission Tag] [References Constructor] [Short] [One Line] Nunc porta lectus commodo rutrum gravida.</permission>
		/// <exception cref="Exception">[Exception Tag] [Short] [One Line] Vestibulum condimentum tincidunt massa, a tincidunt magna facilisis vitae.</exception>
		/// [Floating Comment] [Short] [One Line] Aenean maximus maximus dui, vel ornare est tempor sit amet.
		public ClassSimpleComments()
		{
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Sed eu molestie augue, sed congue sapien.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Nam commodo justo ac porta maximus.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Sed quis dignissim odio, non facilisis felis.</example>
		/// <permission cref="ClassSimpleComments(int,string)">[Permission Tag] [References Constructor] [Short] [One Line] Nullam viverra placerat lacus, vitae bibendum justo interdum quis.</permission>
		/// <exception cref="ArgumentException">[Exception Tag] [Short] [One Line] Vivamus vitae condimentum nisi.</exception>
		/// <param name="a">[Parameter Tag] [Short] [One Line] Nullam bibendum enim nec turpis suscipit porttitor.</param>
		/// <param name="b">[Parameter Tag] [Short] [One Line] Aliquam erat volutpat.</param>
		/// [Floating Comment] [Short] [One Line] Ut dictum massa at iaculis semper.
		public ClassSimpleComments(int a, string b)
		{
		}


		/// <summary>
		/// [Summary Tag] [Short] [One Line] Aenean quam urna, pharetra vel malesuada ut, gravida ac libero.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Pellentesque tincidunt magna sit amet nisl tristique egestas.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Maecenas eu vestibulum mi, sit amet consequat urna.</example>
		/// <permission cref="ClassSimpleComments.Finalize()">[Permission Tag] [References Destructor] [Short] [One Line] Duis accumsan tempor erat ac vehicula.</permission>
		/// <exception cref="IndexOutOfRangeException">[Exception Tag] [Short] [One Line] Duis eu pellentesque orci, nec lobortis magna.</exception>
		/// [Floating Comment] [Short] [One Line] Fusce ullamcorper placerat blandit.
		~ClassSimpleComments()
		{
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Vivamus et ex convallis, egestas justo ut, faucibus arcu.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Maecenas vitae quam quis urna bibendum rhoncus nec id libero.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Sed quis mollis justo, sit amet vulputate justo.</example>
		/// <permission cref="StaticMethodWithParametersWithReturn(int,string)">[Permission Tag] [References Method] [Short] [One Line] In pharetra magna id lorem fermentum rutrum.</permission>
		/// <exception cref="ArgumentException">[Exception Tag] [Short] [One Line] Aliquam et ante non felis facilisis interdum vitae et lacus.</exception>
		/// <param name="a">[Parameter Tag] [Short] [One Line] Nulla consectetur, urna eget ultrices ultricies, velit tortor iaculis lectus, quis condimentum tortor ex vitae ipsum.</param>
		/// <param name="b">[Parameter Tag] [Short] [One Line] Duis ornare sodales convallis.</param>
		/// <returns>[Returns Tag] [Short] [One Line] Maecenas sed orci accumsan, dictum arcu nec, eleifend est.</returns>
		/// [Floating Comment] [Short] [One Line] Suspendisse sed purus et arcu hendrerit posuere at sit amet orci.
		public static string StaticMethodWithParametersWithReturn(int a, string b)
		{
			return "0";
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Maecenas ante nibh, maximus vel laoreet eu, vulputate quis justo.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Maecenas vel nunc fringilla urna pellentesque volutpat quis ac sapien.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Suspendisse ultrices porta sem sed eleifend.</example>
		/// <permission cref="MethodWithoutParameters()">[Permission Tag] [References Method] [Short] [One Line] Suspendisse auctor purus sit amet eros iaculis semper.</permission>
		/// <exception cref="ArgumentException">[Exception Tag] [Short] [One Line] Donec id eros eleifend, eleifend justo sed, laoreet ligula.</exception>
		/// [Floating Comment] [Short] [One Line] In auctor dui et massa sagittis, ut consequat mauris interdum.
		public void MethodWithoutParameters()
		{
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Nunc eu egestas neque, a rutrum nunc.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Mauris finibus eros urna, in gravida metus ullamcorper at.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Vivamus ut risus et nisl blandit cursus nec eu odio.</example>
		/// <permission cref="MethodVirtual()">[Permission Tag] [References Method] [Short] [One Line] Etiam augue enim, pharetra sit amet dictum sit amet, interdum in magna.</permission>
		/// <exception cref="Exception">[Exception Tag] [Short] [One Line] Vestibulum turpis leo, gravida convallis dapibus at, feugiat ac est.</exception>
		/// [Floating Comment] [Short] [One Line] Curabitur euismod condimentum risus, ut pellentesque tortor fringilla in.
		public virtual int MethodVirtual()
		{
			return 0;
		}
	}
}
