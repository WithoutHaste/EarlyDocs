using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Items
{
	/// <summary>
	/// Represents money-type inventory items.
	/// </summary>
	/// <remarks>
	/// Money items have no maximum inventory quantity.
	/// </remarks>
	public class Money : Item
	{
		/// <summary></summary>
		public Money(string name, string shortDescription, string longDescription)
			: base(name, shortDescription, longDescription, NO_MAX_QUANTITY)
		{
		}
	}
}
