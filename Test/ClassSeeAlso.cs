using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// Tests see and seealso tags, i.e. tests links.
	/// </summary>
	/// <see cref="PropertyA"/> or <see cref="PropertyA">Local property</see>
	/// <seealso cref="PropertyA"/> or <seealso cref="PropertyA">Local property</seealso>
	/// <see cref="ClassSeeAlso()"/> or <see cref="ClassSeeAlso()">Local method</see>
	/// <seealso cref="ClassSeeAlso()"/> or <seealso cref="ClassSeeAlso()">Local method</seealso>
	/// <see cref="MethodWithParameters(int,int)"/> or <see cref="MethodWithParameters(int,int)">Local method with parameters</see>
	/// <seealso cref="MethodWithParameters(int,int)"/> or <seealso cref="MethodWithParameters(int,int)">Local method with parameters</seealso>
	/// <see cref="ClassSimpleComments"/> or <see cref="ClassSimpleComments">Another internal type</see>
	/// <seealso cref="ClassSimpleComments"/> or <seealso cref="ClassSimpleComments">Another internal type</seealso>
	/// <see cref="ClassSimpleCommentsGeneric{T,U}"/> or <see cref="ClassSimpleCommentsGeneric{T,U}">Generic internal type</see>
	/// <seealso cref="ClassSimpleCommentsGeneric{T,U}"/> or <seealso cref="ClassSimpleCommentsGeneric{T,U}">Generic internal type</seealso>
	/// <see cref="ClassSimpleComments.IntegerProperty"/> or <see cref="ClassSimpleComments.IntegerProperty">Another internal type's property</see>
	/// <seealso cref="ClassSimpleComments.IntegerProperty"/> or <seealso cref="ClassSimpleComments.IntegerProperty">Another internal type's property</seealso>
	/// <see cref="ClassSimpleComments.ClassSimpleComments()"/> or <see cref="ClassSimpleComments.ClassSimpleComments()">Another internal type's method</see>
	/// <seealso cref="ClassSimpleComments.ClassSimpleComments()"/> or <seealso cref="ClassSimpleComments.ClassSimpleComments()">Another internal type's method</seealso>
	/// <see cref="ClassSimpleComments.ClassSimpleComments(int,string)"/> or <see cref="ClassSimpleComments.ClassSimpleComments(int,string)">Another internal type's method with parameters</see>
	/// <seealso cref="ClassSimpleComments.ClassSimpleComments(int,string)"/> or <seealso cref="ClassSimpleComments.ClassSimpleComments(int,string)">Another internal type's method with parameters</seealso>
	/// <see cref="System.IO.StreamWriter"/> or <see cref="System.IO.StreamWriter">External type</see>
	/// <seealso cref="System.IO.StreamWriter"/> or <seealso cref="System.IO.StreamWriter">External type</seealso>
	/// <see cref="System.IO.StreamWriter.Encoding"/> or <see cref="System.IO.StreamWriter.Encoding">External type's property</see>
	/// <seealso cref="System.IO.StreamWriter.Encoding"/> or <seealso cref="System.IO.StreamWriter.Encoding">External type's property</seealso>
	/// <see cref="System.IO.StreamWriter.Close()"/> or <see cref="System.IO.StreamWriter.Close()">External type's method</see>
	/// <seealso cref="System.IO.StreamWriter.Close()"/> or <seealso cref="System.IO.StreamWriter.Close()">External type's method</seealso>
	/// <see cref="System.IO.StreamWriter.Write(char[])"/> or <see cref="System.IO.StreamWriter.Write(char[])">External type's method with parameters</see>
	/// <seealso cref="System.IO.StreamWriter.Write(char[])"/> or <seealso cref="System.IO.StreamWriter.Write(char[])">External type's method with parameters</seealso>
	public class ClassSeeAlso
	{
		/// <summary></summary>
		public int PropertyA { get; set; }

		/// <summary></summary>
		public ClassSeeAlso()
		{
		}

		/// <summary></summary>
		public string MethodWithParameters(int a, int b)
		{
			return "0";
		}
	}
}
