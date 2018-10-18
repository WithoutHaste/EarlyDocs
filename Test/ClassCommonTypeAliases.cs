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
		public bool FieldBoolean;

		/// <summary></summary>
		public short FieldShort;

		/// <summary></summary>
		public int FieldInt;

		/// <summary></summary>
		public long FieldLong;

		/// <summary></summary>
		public float FieldFloat;

		/// <summary></summary>
		public double FieldDouble;

		/// <summary></summary>
		public string FieldString;

		/// <summary></summary>
		public List<int> FieldList;

		/// <summary></summary>
		public Dictionary<int, string> FieldDictionary;

		/// <summary></summary>
		public Exception FieldException;

		/// <summary></summary>
		public Action<int> FieldAction;

		/// <summary></summary>
		public Func<int, string> FieldFunc;
	}
}
