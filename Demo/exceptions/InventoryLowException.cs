using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Items;

namespace Demo
{
	/// <summary>
	/// Exception related to an inventory quantity being too low.
	/// </summary>
	public class InventoryLowException : Exception
	{
		/// <summary>
		/// The inventory item with low quantity.
		/// </summary>
		public Item Item { get; protected set; }

		/// <summary></summary>
		public int ExpectedQuantity { get; protected set; }

		/// <summary></summary>
		public int ActualQuantity { get; protected set; }

		/// <summary></summary>
		public InventoryLowException(Item item, int expectedQuantity, int actualQuantity) 
			: base(String.Format("Inventory of item {0} too low. Expected {1}, found {2}.", item, expectedQuantity, actualQuantity))
		{
			Item = item;
			ExpectedQuantity = expectedQuantity;
			ActualQuantity = actualQuantity;
		}
	}
}
