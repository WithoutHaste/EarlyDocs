using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// Tests the incorrect nested of comments.
	/// </summary>
	public class ClassCommentsNestedWrong
	{
		/// <summary>
		/// [Summary Tag] [Multiline] Vestibulum a maximus mauris. 
		/// Quisque vel magna et ipsum auctor condimentum vel vitae 
		/// mi. Fusce mi turpis, gravida a aliquam nec, efficitur 
		/// ultrices mi.
		///   <summary>
		///   [Nested Summary Tag] [Multiline] Pellentesque tincidunt nisi ut 
		///   sapien feugiat efficitur. Nunc placerat aliquam erat a iaculis. 
		///   Etiam ullamcorper urna dapibus erat varius finibus.
		///   </summary>
		/// </summary>
		public int SummaryInSummary;

		/// <remarks>
		/// [Remarks Tag] [Multiline] Donec dignissim urna ut 
		/// nisi faucibus, a suscipit dui convallis. Fusce at 
		/// nisi in ligula imperdiet mattis. Sed tincidunt 
		/// consectetur metus et sodales.
		///   <remarks>
		///   [Nested Remarks Tag] [Multiline] Nunc nec justo gravida, 
		///   sodales magna vel, eleifend est. Aliquam viverra massa 
		///   nec diam hendrerit ultrices. Pellentesque eu dolor 
		///   scelerisque, ultrices felis eu, rutrum turpis.
		///   </remarks>
		/// </remarks>
		public int RemarksInRemarks;

	}
}
