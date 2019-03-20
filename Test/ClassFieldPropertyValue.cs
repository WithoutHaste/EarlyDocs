using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// Tests fields/properties with (1) a single summary tag (2) a single value tag (3) using both at once.
	/// </summary>
	public class ClassFieldPropertyValue
	{
		/// <summary>Nulla facilisi.</summary>
		public string SummaryField;

		/// <value>Morbi egestas dolor sapien, ut elementum odio placerat id.</value>
		public string ValueField;

		/// <summary>Fusce ac mi sit amet turpis facilisis venenatis maximus ac nunc.</summary>
		/// <value>Cras euismod vestibulum nunc nec feugiat.</value>
		public string SummaryValueField;

		/// <summary>Nullam nec lorem vitae nunc pellentesque gravida.</summary>
		public string SummaryProperty { get; set; }

		/// <value>Vestibulum at nisi velit.</value>
		public string ValueProperty { get; set; }

		/// <summary>Donec turpis augue, feugiat ut sapien at, sagittis tristique enim.</summary>
		/// <value>Proin mauris eros, laoreet eu tellus a, pulvinar volutpat lorem.</value>
		public string SummaryValueProperty { get; set; }
	}
}
