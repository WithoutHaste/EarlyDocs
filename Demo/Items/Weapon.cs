using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Items
{
	/// <summary>
	/// Represents an inventory item that is a weapon.
	/// </summary>
	public class Weapon : Item, IEquippable
	{
		/// <summary>Standard maximum inventory quantity for weapon items.</summary>
		protected static readonly int MAX_QUANTITY = 5;

		/// <summary>Measure of the damage dealt by the weapon.</summary>
		public int DamageClass { get; protected set; }

		/// <summary></summary>
		public Weapon(string name, string shortDescription, string longDescription, int damageClass)
			: base(name, shortDescription, longDescription, MAX_QUANTITY)
		{
			DamageClass = damageClass;
		}
	}
}
