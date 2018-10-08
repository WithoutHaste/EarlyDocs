using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	/// <summary>
	/// Tests the use of multiple tags for tags where only one is expected: summary, remarks, returns, value.
	/// </summary>
	public class ClassMultipleOnceTags
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
		public int FieldA = 0;

		/// <summary>[1st Summary Tag] Curabitur consequat nec risus vitae convallis.</summary>
		/// <remarks>[1st Remarks Tag] Aenean a cursus arcu.</remarks>
		/// <returns>[1st Returns Tag] Cras sollicitudin laoreet massa, a consequat orci pellentesque sit amet.</returns>
		/// <summary>[2nd Summary Tag] Nulla facilisi.</summary>
		/// <remarks>[2nd Remarks Tag] Vivamus nisl ex, condimentum vitae posuere eget, imperdiet sed diam.</remarks>
		/// <returns>[2nd Returns Tag] Pellentesque a tempus libero.</returns>
		/// <summary>[3rd Summary Tag] Aliquam scelerisque erat risus, ac tincidunt augue egestas quis.</summary>
		/// <remarks>[3rd Remarks Tag] Pellentesque vulputate suscipit molestie.</remarks>
		/// <returns>[3rd Returns Tag] Etiam nunc nunc, consequat sit amet leo vitae, laoreet malesuada tellus.</returns>
		public void MethodA()
		{
		}
	}
}
