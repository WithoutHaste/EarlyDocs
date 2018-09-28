using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Items
{
	/// <summary>
	/// Represents an inventory item that can act as armor.
	/// </summary>
	public class Armor : Item, IEquippable
	{
		/// <summary>Standard maximum inventory quantity for armor items.</summary>
		protected static readonly int MAX_QUANTITY = 10;

		/// <summary>Measure of the strength of the armor.</summary>
		public int ArmorClass { get; protected set; }

		/// <summary></summary>
		public Armor(string name, string shortDescription, string longDescription, int armorClass)
			: base(name, shortDescription, longDescription, MAX_QUANTITY)
		{
			ArmorClass = armorClass;
		}
	}
}
