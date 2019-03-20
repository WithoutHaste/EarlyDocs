using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// Tests the use of multiple tags for tags where only one is expected: summary, remarks, returns, value.
	/// Tests the use of multiple tags for tags where multiple are expected: example, exception, param, typeparam, permission.
	/// </summary>
	/// <permission cref="ClassRepeatedTags">[1st Permission Tag] Aliquam diam ante, ornare et placerat sed, egestas at ante.</permission>
	/// <permission cref="FieldRepeatedOnceOnlyTags">[2nd Permission Tag] Proin sit amet risus nec nulla hendrerit posuere.</permission>
	/// <permission cref="MethodMultipleExceptions()">[3rd Permission Tag] Donec volutpat dignissim vehicula.</permission>
	/// <permission cref="ClassSimpleComments">[4th Permission Tag] Suspendisse varius ipsum at tortor hendrerit suscipit.</permission>
	/// <permission cref="ClassSimpleComments.IntegerProperty">[5th Permission Tag] Suspendisse potenti.</permission>
	/// <permission cref="ClassSimpleComments.Finalize()">[6th Permission Tag] Suspendisse interdum, arcu a venenatis fringilla, elit velit mattis turpis, et gravida augue eros sed erat.</permission>
	public class ClassRepeatedTags
	{
		/// <summary>[1st Summary Tag] Sed ut libero a ante commodo dictum vitae eget urna.</summary>
		/// <remarks>[1st Remarks Tag] Praesent rhoncus porta aliquet.</remarks>
		/// <value>[1st Value Tag] Nulla erat quam, vestibulum sed orci vel, interdum condimentum magna.</value>
		/// <summary>[2nd Summary Tag] Suspendisse a aliquet ex.</summary>
		/// <remarks>[2nd Remarks Tag] Proin venenatis, sapien nec maximus dictum, velit nisi condimentum velit, non luctus leo metus eget arcu.</remarks>
		/// <value>[2nd Value Tag] Duis dapibus ultrices arcu, sed aliquam velit egestas quis.</value>
		/// <summary>[3rd Summary Tag] Fusce porttitor ultrices volutpat.</summary>
		/// <remarks>[3rd Remarks Tag] Proin quis nunc lacus.</remarks>
		/// <value>[3rd Value Tag] Donec aliquet arcu neque, vitae laoreet lectus viverra ut.</value>
		public int FieldRepeatedOnceOnlyTags = 0;

		/// <example>[1st Example Tag] Ut tincidunt ipsum sit amet eros dictum, sed sollicitudin nisl interdum.</example>
		/// <example>[2nd Example Tag] Fusce vel lacus tristique, maximus tellus ut, bibendum ante. Aenean volutpat efficitur quam, ac dignissim elit elementum sodales. Etiam pellentesque eu metus in pharetra. Morbi et iaculis nunc, id cursus tellus. Aenean elementum molestie laoreet. Cras at ipsum id enim commodo posuere.</example>
		/// <example>
		/// [3rd Example Tag] Vestibulum elementum pulvinar fringilla. 
		/// Curabitur nisi orci, iaculis in dolor non, interdum aliquam 
		/// tellus. Fusce ac dui tempor, porttitor diam sit amet, 
		/// ullamcorper quam. Sed sed enim vitae velit accumsan maximus. 
		/// 
		/// Quisque bibendum cursus lacus id tempus. Quisque dignissim 
		/// molestie efficitur. Aliquam a turpis elementum, gravida sem 
		/// sit amet, cursus justo. In sit amet facilisis nisi. In 
		/// vehicula facilisis ipsum, vel faucibus justo.
		/// </example>
		/// <example>[4th Example Tag] Fusce tincidunt vitae libero luctus venenatis. Aenean semper ultrices enim eu bibendum.</example>
		/// <example>[5th Example Tag] Aliquam posuere sapien vitae lorem semper, at vestibulum velit pellentesque.</example>
		/// <example>[6th Example Tag] Fusce sit amet lacinia risus, at efficitur neque. Integer enim lorem, interdum maximus sollicitudin vitae, mollis at ante. In faucibus dolor erat, at efficitur leo dignissim quis. Aenean metus velit, convallis vel dignissim ut, fermentum vitae augue. Ut nunc erat, auctor ut leo at, tincidunt vestibulum leo. Quisque euismod nibh magna, dignissim sagittis dolor porta non. Phasellus a ornare nulla. Praesent auctor rutrum justo quis semper. Sed posuere cursus facilisis.</example>
		public float PropertyMultipleExamples { get; set; }

		/// <summary>[1st Summary Tag] Curabitur consequat nec risus vitae convallis.</summary>
		/// <remarks>[1st Remarks Tag] Aenean a cursus arcu.</remarks>
		/// <returns>[1st Returns Tag] Cras sollicitudin laoreet massa, a consequat orci pellentesque sit amet.</returns>
		/// <summary>[2nd Summary Tag] Nulla facilisi.</summary>
		/// <remarks>[2nd Remarks Tag] Vivamus nisl ex, condimentum vitae posuere eget, imperdiet sed diam.</remarks>
		/// <returns>[2nd Returns Tag] Pellentesque a tempus libero.</returns>
		/// <summary>[3rd Summary Tag] Aliquam scelerisque erat risus, ac tincidunt augue egestas quis.</summary>
		/// <remarks>[3rd Remarks Tag] Pellentesque vulputate suscipit molestie.</remarks>
		/// <returns>[3rd Returns Tag] Etiam nunc nunc, consequat sit amet leo vitae, laoreet malesuada tellus.</returns>
		public void MethodRepeatedOnceOnlyTags()
		{
		}

		/// <exception cref="ArgumentException">[1st Exception Tag] Nullam feugiat odio felis.</exception>
		/// <exception cref="ArgumentException">[2nd Exception Tag] Fusce dui elit, iaculis ut venenatis ac, laoreet eget nisi.</exception>
		/// <exception cref="ArgumentException">[3rd Exception Tag] Nulla sed magna sed velit dictum auctor.</exception>
		/// <exception cref="System.IO.FileNotFoundException">[4th Exception Tag] Sed et mauris ut purus bibendum congue eu a quam.</exception>
		/// <exception cref="ClassException">[5th Exception Tag] Nullam fermentum libero placerat lacus tempor consequat.</exception>
		public void MethodMultipleExceptions()
		{
		}

		/// <param name="a">[1st Param Tag] Maecenas volutpat elit ut congue vulputate.</param>
		/// <param name="b">[2nd Param Tag] Nunc vehicula risus sed egestas volutpat.</param>
		/// <param name="c">[3rd Param Tag] Sed porttitor tempor nisi, ut luctus felis tristique vel.</param>
		/// <param name="f">[4th Param Tag] Aliquam nisl leo, condimentum id aliquam in, pretium at dolor.</param>
		/// <param name="e">[5th Param Tag] Nullam ultrices eleifend risus a condimentum.</param>
		/// <param name="d">[6th Param Tag] Suspendisse quis dui sit amet mi laoreet mollis.</param>
		public void MethodMultipleParams(int a, string b, int c, float d, List<List<int>> e, bool f)
		{
		}

		/// <typeparam name="A">[1st TypeParam Tag] Curabitur eget velit porta, bibendum metus vitae, venenatis enim.</typeparam>
		/// <typeparam name="B">[2nd TypeParam Tag] Duis lacus velit, aliquet sagittis sem eget, malesuada blandit nisl.</typeparam>
		/// <typeparam name="C">[3rd TypeParam Tag] Aenean tempor viverra dolor, quis congue enim feugiat in.</typeparam>
		/// <typeparam name="F">[4th TypeParam Tag] Pellentesque ultricies porta nunc a tempus.</typeparam>
		/// <typeparam name="E">[5th TypeParam Tag] Aliquam erat volutpat.</typeparam>
		/// <typeparam name="D">[6th TypeParam Tag] Vestibulum ac fringilla felis.</typeparam>
		public void MethodMultipleTypeParams<A,B,C,D,E,F>()
		{
		}

	}
}
