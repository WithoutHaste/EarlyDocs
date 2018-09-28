using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Items
{
	/// <summary>
	/// Collection of standard inventory items.
	/// </summary>
	public static class StandardItems
	{
		/// <summary>Copper coins.</summary>
		public static readonly Money COPPER = new Money("Copper", "Copper coins.", "Copper coins, the smallest unit of currency in the kingdom.");
		/// <summary>Silver coins.</summary>
		public static readonly Money SILVER = new Money("Silver", "Silver coins.", "Silver coins have a hawk on one side and the king's profile on the other.");
		/// <summary>Gold coins.</summary>
		public static readonly Money GOLD = new Money("Gold", "Gold coins.", "Gold coins are embossed with the god Mercury on both sides.");

		/// <summary>Level 1 leather armor.</summary>
		public static readonly Armor ARMOR_LEATHER_LV1 = new Armor("Leather Armor", "Level 1 leather armor.", "Leather armor, heavily worn and repaired many times.", 1);

		/// <summary>Level 1 short sword.</summary>
		public static readonly Weapon SWORD_SHORT_LV1 = new Weapon("Short Sword", "Level 1 short sword.", "A short sword, slightly dull but clean of rust.", 5);
	}
}
