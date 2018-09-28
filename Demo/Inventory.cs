using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Demo.Items;

namespace Demo
{
	/// <summary>
	/// Represents a character's inventory.
	/// </summary>
	public class Inventory
	{
		private Dictionary<Item, int> itemQuantities = new Dictionary<Item, int>();

		/// <summary></summary>
		public Inventory()
		{
		}

		/// <summary>
		/// Add more units of an item to inventory.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="quantity">Amount to add to inventory.</param>
		/// <exception cref="ArgumentException"><paramref name="quantity"/> cannot be less than 1.</exception>
		public void Increment(Item item, int quantity = 1)
		{
			if(quantity <= 0)
				throw new ArgumentException("Increment quantity cannot be less than 1.", "quantity");
			if(!itemQuantities.ContainsKey(item))
				itemQuantities[item] = 0;
			itemQuantities[item] += quantity;
			if(itemQuantities[item] > item.MaxQuantity)
				itemQuantities[item] = item.MaxQuantity;
		}

		/// <summary>
		/// Remove units of an item from inventory.
		/// </summary>
		/// <param name="item"></param>
		/// <param name="quantity">Amount to remove from inventory.</param>
		/// <exception cref="ArgumentException"><paramref name="quantity"/> cannot be less than 1.</exception>
		/// <exception cref="InventoryLowException">There are not enough units of <paramref name="item"/> in inventory.</exception>
		public void Decrement(Item item, int quantity = 1)
		{
			if(quantity <= 0)
				throw new ArgumentException("Decrement quantity cannot be less than 1.", "quantity");
			if(!itemQuantities.ContainsKey(item))
				throw new InventoryLowException(item, quantity, 0);
			else if(itemQuantities[item] < quantity)
				throw new InventoryLowException(item, quantity, itemQuantities[item]);
			itemQuantities[item] -= quantity;
		}

	}
}
