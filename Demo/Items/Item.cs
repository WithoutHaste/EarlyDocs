using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Items
{
	/// <summary>
	/// Represents an item that can be held in inventory.
	/// </summary>
	public abstract class Item
	{
		/// <summary>Indicates item has no limit on inventory quantity.</summary>
		protected const int NO_MAX_QUANTITY = -1;

		/// <summary></summary>
		public string Name { get; protected set; }

		/// <summary></summary>
		public string ShortDescription { get; protected set; }

		/// <summary></summary>
		public string LongDescription { get; protected set; }

		/// <summary>
		/// The maximum number of this item that can be held in inventory at one time.
		/// </summary>
		public int MaxQuantity { get; protected set; }

		/// <summary></summary>
		public Item(string name, string shortDescription, string longDescription, int maxQuantity = NO_MAX_QUANTITY)
		{
			Name = name;
			ShortDescription = shortDescription;
			LongDescription = longDescription;
			MaxQuantity = maxQuantity;
		}
	}
}
