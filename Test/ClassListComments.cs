using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
	/// <summary>
	/// Tests lists and tables in comments.
	/// </summary>
	public class ClassListComments
	{
		/// <summary>
		///   Previous text:
		///   <list type="bullet">
		///     <listheader><term>Header Term</term><description>Header Description</description></listheader>
		///     <item><term>Term A</term><description>Description A</description></item>
		///     <item><term>Term B</term><description>Description B</description></item>
		///     <item><term>Term C</term><description>Description C</description></item>
		///   </list>
		/// </summary>
		/// <remarks>
		///   <list type="bullet">
		///     <item><term>Term A</term><description>Description A</description></item>
		///     <item><term>Term B</term><description>Description B</description></item>
		///     <item><term>Term C</term><description>Description C</description></item>
		///   </list>
		/// </remarks>
		/// <example>
		///   <list type="bullet">
		///     <item><description>Description A</description></item>
		///     <item><description>Description B</description></item>
		///     <item><description>Description C</description></item>
		///   </list>
		/// </example>
		/// <example>
		///   <list type="bullet">
		///     <item>Description A</item>
		///     <item>Description B</item>
		///     <item>Description C</item>
		///   </list>
		/// </example>
		public int BulletList = 0;

		/// <summary>
		///   Previous text:
		///   <list type="number">
		///     <listheader><term>Header Term</term><description>Header Description</description></listheader>
		///     <item><term>Term A</term><description>Description A</description></item>
		///     <item><term>Term B</term><description>Description B</description></item>
		///     <item><term>Term C</term><description>Description C</description></item>
		///   </list>
		/// </summary>
		/// <remarks>
		///   <list type="number">
		///     <item><term>Term A</term><description>Description A</description></item>
		///     <item><term>Term B</term><description>Description B</description></item>
		///     <item><term>Term C</term><description>Description C</description></item>
		///   </list>
		/// </remarks>
		/// <example>
		///   <list type="number">
		///     <item><description>Description A</description></item>
		///     <item><description>Description B</description></item>
		///     <item><description>Description C</description></item>
		///   </list>
		/// </example>
		/// <example>
		///   <list type="number">
		///     <item>Description A</item>
		///     <item>Description B</item>
		///     <item>Description C</item>
		///   </list>
		/// </example>
		public int NumberList = 0;

		/// <summary>
		///   Previous text:
		///   <list type="table">
		///     <listheader>
		///       <term>Header 1</term>
		///       <term>Header 2</term>
		///       <term>Header 3</term>
		///     </listheader>
		///     <item>
		///		  <term>Row 1, Cell 1</term>
		///		  <term>Row 1, Cell 2</term>
		///		  <term>Row 1, Cell 3</term>
		///		</item>
		///     <item>
		///		  <term>Row 2, Cell 1</term>
		///		  <term>Row 2, Cell 2</term>
		///		  <term>Row 2, Cell 3</term>
		///		</item>
		///   </list>
		/// </summary>
		/// <remarks>
		///   <list type="table">
		///     <item>
		///		  <term>Row 1, Cell 1</term>
		///		  <term>Row 1, Cell 2</term>
		///		  <term>Row 1, Cell 3</term>
		///		</item>
		///     <item>
		///		  <term>Row 2, Cell 1</term>
		///		  <term>Row 2, Cell 2</term>
		///		  <term>Row 2, Cell 3</term>
		///		</item>
		///   </list>
		/// </remarks>
		public int Table = 0;

		/// <summary>
		///   Previous text:
		///   <list type="bullet">
		///     <listheader><term>Header Term</term><description>Header Description</description></listheader>
		///     <item><term>Term A</term><description>Description A</description></item>
		///     <item><term>Term B</term><description>Description B</description></item>
		///   <list type="number">
		///     <item>Description A</item>
		///     <item>Description B</item>
		///     <item>Description C</item>
		///   </list>
		///     <item><term>Term C</term><description>Description C</description></item>
		///   </list>
		/// </summary>
		public int NumberListInBulletList = 0;

		/// <summary>
		///   Previous text:
		///   <list type="bullet">
		///     <listheader><term>Header Term</term><description>Header Description</description></listheader>
		///     <item><term>Term A</term><description>Description A</description></item>
		///     <item>
		///   <list type="number">
		///     <item>Description A</item>
		///     <item>Description B</item>
		///     <item>Description C</item>
		///   </list>
		///		</item>
		///     <item><term>Term C</term><description>Description C</description></item>
		///   </list>
		/// </summary>
		public int NumberListInBulletItem = 0;

		/// <summary>
		///   Previous text:
		///   <list type="number">
		///     <listheader><term>Header Term</term><description>Header Description</description></listheader>
		///     <item><term>Term A</term><description>Description A</description></item>
		///     <item><term>Term B</term><description>Description B</description></item>
		///   <list type="bullet">
		///     <item><term>Term A</term><description>Description A</description></item>
		///     <item><term>Term B</term><description>Description B</description></item>
		///     <item><term>Term C</term><description>Description C</description></item>
		///   </list>
		///     <item><term>Term C</term><description>Description C</description></item>
		///   </list>
		/// </summary>
		public int BulletListInNumberList = 0;

		/// <summary>
		///   Previous text:
		///   <list type="number">
		///     <listheader><term>Header Term</term><description>Header Description</description></listheader>
		///     <item><term>Term A</term><description>Description A</description></item>
		///     <item>
		///   <list type="bullet">
		///     <item><term>Term A</term><description>Description A</description></item>
		///     <item><term>Term B</term><description>Description B</description></item>
		///     <item><term>Term C</term><description>Description C</description></item>
		///   </list>
		///		</item>
		///     <item><term>Term C</term><description>Description C</description></item>
		///   </list>
		/// </summary>
		public int BulletListInNumberItem = 0;

		/// <summary>
		/// Previous text:
		/// <list type='number'>
		///		<item>Riccasus</item>
		///		<item>Lorem <see cref='ClassSeeAlso'/></item>
		///		<item>Finitini <see cref='ClassSeeAlso.MethodWithParameters(int,int)'/></item>
		/// </list>
		/// </summary>
		public int InlineTagsInList = 0;
	}
}
