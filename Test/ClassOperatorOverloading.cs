using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	/// <summary>
	/// Tests operator overloading.
	/// </summary>
	public class ClassOperatorOverloading
	{
		/// <summary>
		/// [Summary Tag] [Short] [One Line] Nam sit amet egestas orci.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] In a fermentum nunc.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Sed massa justo, laoreet quis magna at, iaculis dictum justo.</example>
		/// <permission cref="implicit operator double(ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Interdum et malesuada fames ac ante ipsum primis in faucibus.</permission>
		/// <exception cref="ArgumentException">[Exception Tag] [Short] [One Line] Sed ac molestie ante, vel porttitor neque.</exception>
		/// <param name="a">[Parameter Tag] [Short] [One Line] Vestibulum quis fringilla ipsum.</param>
		/// <returns>[Returns Tag] [Short] [One Line] Donec in lobortis orci.</returns>
		/// [Floating Comment] [Short] [One Line] Maecenas sagittis nec metus a fermentum.
		public static implicit operator double(ClassOperatorOverloading a)
		{
			return 0;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Fusce at mollis urna.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Fusce accumsan augue dui.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] In laoreet mauris nec purus venenatis accumsan.</example>
		/// <permission cref="implicit operator int(ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Etiam dictum cursus elementum.</permission>
		/// <exception cref="ArgumentException">[Exception Tag] [Short] [One Line] Donec malesuada ex sit amet mollis facilisis.</exception>
		/// <param name="a">[Parameter Tag] [Short] [One Line] Vestibulum semper maximus turpis, quis sodales nisl mattis ac.</param>
		/// <returns>[Returns Tag] [Short] [One Line] Vivamus fermentum turpis in tellus dignissim, vel aliquam nunc venenatis.</returns>
		/// [Floating Comment] [Short] [One Line] Proin in odio turpis.
		public static implicit operator int(ClassOperatorOverloading a)
		{
			return 0;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Maecenas mattis fringilla felis, a lacinia quam commodo id.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Quisque et mattis leo.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Nulla imperdiet diam ut felis ultricies, tempor scelerisque odio ornare.</example>
		/// <permission cref="explicit operator float(ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Phasellus euismod quam ut lacus tincidunt suscipit.</permission>
		/// <exception cref="ArgumentException">[Exception Tag] [Short] [One Line] Nullam eu libero arcu.</exception>
		/// <param name="a">[Parameter Tag] [Short] [One Line] Vivamus volutpat, elit sit amet gravida rutrum, quam tortor vulputate risus, sed blandit dolor lacus non diam.</param>
		/// <returns>[Returns Tag] [Short] [One Line] Donec convallis, tortor eu porttitor fermentum, felis ligula blandit neque, ac ultrices quam massa eget lacus.</returns>
		/// [Floating Comment] [Short] [One Line] Proin accumsan, dolor non consequat ultricies, tellus nulla lobortis massa, porttitor iaculis ipsum erat ac nibh.
		public static explicit operator float(ClassOperatorOverloading a)
		{
			return 0;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Nunc imperdiet ex et leo porttitor fermentum.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Duis varius gravida metus id ultrices.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Ut vestibulum tellus felis, auctor venenatis turpis blandit sit amet.</example>
		/// <permission cref="explicit operator short(ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Donec vel dictum velit, eget porta arcu.</permission>
		/// <exception cref="ArgumentException">[Exception Tag] [Short] [One Line] Aliquam hendrerit, ligula quis faucibus venenatis, libero leo venenatis lacus, vulputate euismod erat turpis nec ex.</exception>
		/// <param name="a">[Parameter Tag] [Short] [One Line] In hac habitasse platea dictumst.</param>
		/// <returns>[Returns Tag] [Short] [One Line] Quisque at quam nulla.</returns>
		/// [Floating Comment] [Short] [One Line] Quisque et efficitur nunc.
		public static explicit operator short(ClassOperatorOverloading a)
		{
			return 0;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Praesent id pulvinar nunc.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Vestibulum eget magna ut turpis pellentesque lobortis.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Phasellus eget aliquet tellus.</example>
		/// <permission cref="operator +(ClassOperatorOverloading,ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Pellentesque viverra rutrum molestie.</permission>
		/// <exception cref="ArgumentException">[Exception Tag] [Short] [One Line] Curabitur et lacinia felis.</exception>
		/// <param name="a">[Parameter Tag] [Short] [One Line] Vivamus nec felis tortor.</param>
		/// <param name="b">[Parameter Tag] [Short] [One Line] Nam maximus odio vel consequat congue.</param>
		/// <returns>[Returns Tag] [Short] [One Line] Etiam nec ex ac ante gravida gravida eu pulvinar risus.</returns>
		/// [Floating Comment] [Short] [One Line] Morbi viverra arcu et sapien maximus varius.
		public static ClassOperatorOverloading operator +(ClassOperatorOverloading a, ClassOperatorOverloading b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Sed hendrerit eros eget purus dictum, viverra feugiat eros porttitor.
		/// </summary>
		/// <remarks>
		/// [Remarks Tag] [Short] [One Line] Nunc a odio sodales augue congue posuere ut a ex.
		/// </remarks>
		/// <example>[Example Tag] [Short] [One Line] Maecenas lectus velit, maximus vel ipsum eu, volutpat tempor tortor.</example>
		/// <permission cref="operator +(ClassOperatorOverloading,int)">[Permission Tag] [References Operator] [Short] [One Line] Sed at nisi leo.</permission>
		/// <exception cref="ArgumentException">[Exception Tag] [Short] [One Line] Quisque vitae purus elit.</exception>
		/// <param name="a">[Parameter Tag] [Short] [One Line] Pellentesque est metus, euismod sit amet viverra eget, tincidunt consectetur massa.</param>
		/// <param name="b">[Parameter Tag] [Short] [One Line] Maecenas maximus congue neque, at iaculis justo dignissim nec.</param>
		/// <returns>[Returns Tag] [Short] [One Line] Maecenas tempor quam nisi, sit amet eleifend enim consectetur et.</returns>
		/// [Floating Comment] [Short] [One Line] Pellentesque vel malesuada mi, quis pharetra erat.
		public static ClassOperatorOverloading operator +(ClassOperatorOverloading a, int b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Donec dictum lacus tortor, at mollis est tempor ut.
		/// </summary>
		/// <permission cref="operator -(ClassOperatorOverloading,ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Aenean tempor nulla a tellus interdum, in commodo nisi sagittis.</permission>
		public static ClassOperatorOverloading operator -(ClassOperatorOverloading a, ClassOperatorOverloading b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Quisque quis tortor dapibus, posuere ipsum mattis, efficitur eros.
		/// </summary>
		/// <permission cref="operator -(ClassOperatorOverloading,int)">[Permission Tag] [References Operator] [Short] [One Line] Nunc faucibus sem tortor, sed pharetra ligula congue vel.</permission>
		public static ClassOperatorOverloading operator -(ClassOperatorOverloading a, int b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Duis a urna at purus placerat bibendum.
		/// </summary>
		/// <permission cref="operator *(ClassOperatorOverloading,ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Etiam iaculis nibh a risus tempus, non semper magna volutpat.</permission>
		public static ClassOperatorOverloading operator *(ClassOperatorOverloading a, ClassOperatorOverloading b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Suspendisse nec felis sagittis, rhoncus dolor sit amet, varius ipsum.
		/// </summary>
		/// <permission cref="operator *(ClassOperatorOverloading,int)">[Permission Tag] [References Operator] [Short] [One Line] Suspendisse potenti.</permission>
		public static ClassOperatorOverloading operator *(ClassOperatorOverloading a, int b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Suspendisse ac gravida nunc, sit amet consequat orci.
		/// </summary>
		/// <permission cref="operator /(ClassOperatorOverloading,ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Pellentesque metus lacus, aliquet vitae lorem vel, accumsan lacinia ante.</permission>
		public static ClassOperatorOverloading operator /(ClassOperatorOverloading a, ClassOperatorOverloading b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Donec consectetur diam in felis elementum, a iaculis lacus semper.
		/// </summary>
		/// <permission cref="operator /(ClassOperatorOverloading,int)">[Permission Tag] [References Operator] [Short] [One Line] Integer varius, sem vitae bibendum elementum, felis tellus eleifend tellus, sed commodo sem lorem a risus.</permission>
		public static ClassOperatorOverloading operator /(ClassOperatorOverloading a, int b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Aliquam sit amet sapien a odio consequat egestas sit amet ac nulla.
		/// </summary>
		/// <permission cref="operator %(ClassOperatorOverloading,ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Fusce imperdiet ante ac nunc aliquet semper.</permission>
		public static ClassOperatorOverloading operator %(ClassOperatorOverloading a, ClassOperatorOverloading b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Phasellus malesuada elit orci, eget tincidunt arcu placerat sit amet.
		/// </summary>
		/// <permission cref="operator %(ClassOperatorOverloading,int)">[Permission Tag] [References Operator] [Short] [One Line] Nullam ultricies urna id semper egestas.</permission>
		public static ClassOperatorOverloading operator %(ClassOperatorOverloading a, int b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Cras vulputate est ante, scelerisque dignissim neque porttitor sed.
		/// </summary>
		/// <permission cref="op_BitwiseAnd(ClassOperatorOverloading,ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Nulla sed ex tellus.</permission>
		public static ClassOperatorOverloading operator &(ClassOperatorOverloading a, ClassOperatorOverloading b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Ut elit enim, posuere id nunc ac, fermentum congue nibh.
		/// </summary>
		/// <permission cref="op_BitwiseAnd(ClassOperatorOverloading,int)">[Permission Tag] [References Operator] [Short] [One Line] Suspendisse porta interdum nisi quis luctus.</permission>
		public static ClassOperatorOverloading operator &(ClassOperatorOverloading a, int b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Curabitur vel turpis vel tellus luctus sagittis.
		/// </summary>
		/// <permission cref="operator |(ClassOperatorOverloading,ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Donec dictum venenatis enim at venenatis.</permission>
		public static ClassOperatorOverloading operator |(ClassOperatorOverloading a, ClassOperatorOverloading b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae.
		/// </summary>
		/// <permission cref="operator |(ClassOperatorOverloading,int)">[Permission Tag] [References Operator] [Short] [One Line] Donec sagittis, nulla non lobortis egestas, lectus quam euismod purus, nec luctus elit justo eu libero.</permission>
		public static ClassOperatorOverloading operator |(ClassOperatorOverloading a, int b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Donec id mollis orci.
		/// </summary>
		/// <permission cref="operator true(ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Donec molestie condimentum purus, vitae condimentum diam dignissim et.</permission>
		public static bool operator true(ClassOperatorOverloading a)
		{
			return true;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Pellentesque consectetur libero a ligula tempus convallis.
		/// </summary>
		/// <permission cref="operator false(ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Nullam ullamcorper purus in velit tempus, in scelerisque erat porta.</permission>
		public static bool operator false(ClassOperatorOverloading a)
		{
			return true;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Nam euismod euismod consectetur.
		/// </summary>
		/// <permission cref="operator ^(ClassOperatorOverloading,ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Fusce ornare aliquam lorem, at commodo nisl suscipit nec.</permission>
		public static ClassOperatorOverloading operator ^(ClassOperatorOverloading a, ClassOperatorOverloading b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Suspendisse potenti.
		/// </summary>
		/// <permission cref="operator ^(ClassOperatorOverloading,int)">[Permission Tag] [References Operator] [Short] [One Line] In eget vehicula massa.</permission>
		public static ClassOperatorOverloading operator ^(ClassOperatorOverloading a, int b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Proin id felis at turpis rhoncus ultrices sit amet in ex.
		/// </summary>
		/// <permission cref="operator ~(ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Aenean eget velit augue.</permission>
		public static ClassOperatorOverloading operator ~(ClassOperatorOverloading a)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Duis eget velit vitae nisi maximus mollis.
		/// </summary>
		/// <permission cref="operator !(ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Praesent mauris arcu, gravida elementum nibh in, pellentesque fermentum lacus.</permission>
		public static ClassOperatorOverloading operator !(ClassOperatorOverloading a)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Nam maximus vehicula nisi non lacinia.
		/// </summary>
		/// <permission cref="operator ==(ClassOperatorOverloading,ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Curabitur ut purus nulla.</permission>
		public static bool operator ==(ClassOperatorOverloading a, ClassOperatorOverloading b)
		{
			return true;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Vestibulum ante ipsum primis in faucibus orci luctus et ultrices posuere cubilia Curae.
		/// </summary>
		/// <permission cref="operator ==(ClassOperatorOverloading,int)">[Permission Tag] [References Operator] [Short] [One Line] Maecenas sed felis blandit, rhoncus ante vel, mollis orci.</permission>
		public static bool operator ==(ClassOperatorOverloading a, int b)
		{
			return true;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus.
		/// </summary>
		/// <permission cref="operator !=(ClassOperatorOverloading,ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Nulla at iaculis augue.</permission>
		public static bool operator !=(ClassOperatorOverloading a, ClassOperatorOverloading b)
		{
			return true;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Sed porttitor gravida ligula, et semper libero euismod eget.
		/// </summary>
		/// <permission cref="operator !=(ClassOperatorOverloading,int)">[Permission Tag] [References Operator] [Short] [One Line] Nam sollicitudin molestie urna eu condimentum.</permission>
		public static bool operator !=(ClassOperatorOverloading a, int b)
		{
			return true;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Suspendisse potenti.
		/// </summary>
		/// <permission cref="operator >(ClassOperatorOverloading,ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Etiam rutrum sed leo quis vestibulum.</permission>
		public static bool operator >(ClassOperatorOverloading a, ClassOperatorOverloading b)
		{
			return true;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Aenean sodales pharetra odio, at volutpat turpis feugiat id.
		/// </summary>
		/// <permission cref="operator >(ClassOperatorOverloading,int)">[Permission Tag] [References Operator] [Short] [One Line] Quisque est purus, aliquet id lacinia nec, scelerisque et sem.</permission>
		public static bool operator >(ClassOperatorOverloading a, int b)
		{
			return true;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] In in dignissim magna.
		/// </summary>
		/// <permission cref="op_LessThan(ClassOperatorOverloading,ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Cras quis mauris eu nibh luctus egestas eget eu ex.</permission>
		public static bool operator <(ClassOperatorOverloading a, ClassOperatorOverloading b)
		{
			return true;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Quisque a massa purus.
		/// </summary>
		/// <permission cref="op_LessThan(ClassOperatorOverloading,int)">[Permission Tag] [References Operator] [Short] [One Line] Etiam nisl felis, bibendum quis ipsum ut, cursus hendrerit augue.</permission>
		public static bool operator <(ClassOperatorOverloading a, int b)
		{
			return true;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Morbi purus lacus, dignissim gravida imperdiet egestas, rutrum sed nibh.
		/// </summary>
		/// <permission cref=">=(ClassOperatorOverloading,ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Vivamus tempus tincidunt nulla et commodo.</permission>
		public static bool operator >=(ClassOperatorOverloading a, ClassOperatorOverloading b)
		{
			return true;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Nunc non diam luctus, consectetur purus vitae, commodo quam.
		/// </summary>
		/// <permission cref=">=(ClassOperatorOverloading,int)">[Permission Tag] [References Operator] [Short] [One Line] In elementum, mi non viverra molestie, lorem felis interdum massa, at varius quam ex in massa.</permission>
		public static bool operator >=(ClassOperatorOverloading a, int b)
		{
			return true;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Morbi placerat ut metus sollicitudin bibendum.
		/// </summary>
		/// <permission cref="op_LessThanOrEqual(ClassOperatorOverloading,ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Sed vestibulum justo mauris, quis rhoncus dolor tristique porttitor.</permission>
		public static bool operator <=(ClassOperatorOverloading a, ClassOperatorOverloading b)
		{
			return true;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Donec semper convallis malesuada.
		/// </summary>
		/// <permission cref="op_LessThanOrEqual(ClassOperatorOverloading,int)">[Permission Tag] [References Operator] [Short] [One Line] Duis ullamcorper sapien mi, ac convallis erat vestibulum a.</permission>
		public static bool operator <=(ClassOperatorOverloading a, int b)
		{
			return true;
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Vestibulum euismod consequat sodales.
		/// </summary>
		/// <permission cref="operator --(ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Proin molestie dapibus sem quis varius.</permission>
		public static ClassOperatorOverloading operator --(ClassOperatorOverloading a)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Mauris felis nisi, consequat sit amet vulputate sed, consectetur ac libero.
		/// </summary>
		/// <permission cref="operator ++(ClassOperatorOverloading)">[Permission Tag] [References Operator] [Short] [One Line] Morbi finibus ullamcorper pellentesque.</permission>
		public static ClassOperatorOverloading operator ++(ClassOperatorOverloading a)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Etiam faucibus, dolor ac condimentum scelerisque, quam libero faucibus nisi, non sagittis lacus augue quis odio.
		/// </summary>
		/// <permission cref="op_LeftShift(ClassOperatorOverloading,int)">[Permission Tag] [References Operator] [Short] [One Line] Quisque egestas est nec nibh tempus ultrices.</permission>
		public static ClassOperatorOverloading operator <<(ClassOperatorOverloading a, int b)
		{
			return new ClassOperatorOverloading();
		}

		/// <summary>
		/// [Summary Tag] [Short] [One Line] Suspendisse mattis pretium ligula sit amet elementum.
		/// </summary>
		/// <permission cref="operator >>(ClassOperatorOverloading,int)">[Permission Tag] [References Operator] [Short] [One Line] Suspendisse sit amet arcu erat.</permission>
		public static ClassOperatorOverloading operator >>(ClassOperatorOverloading a, int b)
		{
			return new ClassOperatorOverloading();
		}

	}
}
