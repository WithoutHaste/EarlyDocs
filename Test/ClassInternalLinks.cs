using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// Tests the automatic internal links between types in this library.
	/// </summary>
	public class ClassInternalLinks
	{
		/// <summary></summary>
		public ClassSimpleComments FieldType;

		/// <summary></summary>
		public ClassSimpleComments[] FieldTypeArray;

		/// <summary></summary>
		public List<ClassSimpleComments> FieldTypeList;

		/// <summary></summary>
		public ClassSimpleComments PropertyType { get; set; }

		/// <summary></summary>
		public ClassSimpleComments[] PropertyTypeArray { get; set; }

		/// <summary></summary>
		public List<ClassSimpleComments> PropertyTypeList { get; set; }
	}
}
