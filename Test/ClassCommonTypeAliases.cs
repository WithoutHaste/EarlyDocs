using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	/// <summary>
	/// Tests the display of common data types that have recognized aliases in .Net.
	/// Also common data types that have long fully-qualified names.
	/// </summary>
	public class ClassCommonTypeAliases
	{
		/// <summary></summary>
		public short ShortField = 0;

		/// <summary></summary>
		public int IntegerField = 0;

		/// <summary></summary>
		public double DoubleField = 0;

		/// <summary></summary>
		public float FloatField = 0;

		/// <summary></summary>
		public string StringField = "0";

		/// <summary></summary>
		public List<int> ListIntegerField = new List<int>();
	}
}
