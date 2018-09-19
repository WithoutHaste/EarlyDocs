using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo
{
	/// <summary>
	/// Summary of the enum.
	/// </summary>
	public enum GlobalEnum {
		/// <summary>Invalid selection.</summary>
		Invalid = 0,
		/// <summary></summary>
		NoSummary,
		/// <summary>A short summary.</summary>
		ShortSummary,
		/// <summary>
		/// A very long summary without line breaks: There was no possibility of taking a walk that day. We had been wandering, indeed, in the leafless shrubbery an hour in the morning; but since dinner (Mrs. Reed, when there was no company, dined early) the cold winter wind had brought with it clouds so sombre, and a rain so penetrating, that further out-door exercise was now out of the question
		/// </summary>
		LongSummary,
		/// <summary>
		/// A very long summary with line breaks: There was no possibility of taking a walk that day. 
		/// We had been wandering, indeed, in the leafless shrubbery an hour in the morning; but 
		/// since dinner (Mrs. Reed, when there was no company, dined early) the cold winter wind 
		/// had brought with it clouds so sombre, and a rain so penetrating, that further out-door 
		/// exercise was now out of the question
		/// </summary>
		MultiLineSummary,
		/// <summary>
		/// Summary 1 of 3.
		/// </summary>
		/// <summary>
		/// Summary 2 of 3.
		/// </summary>
		/// <summary>
		/// Summary 3 of 3.
		/// </summary>
		MultipleSummaries
	}

	/// <summary>
	/// Summary of the class.
	/// </summary>
	/// <remarks>Remarks about the class.
	/// </remarks>
	/// <example>Class can be used this way.</example>
	/// <example>Class can be used that way.</example>
	public class NormalClass
	{
		/// <summary>
		/// Summary of enum that has no summaries on the values.
		/// </summary>
		public enum ClassEnum {
			/// <summary></summary>
			Invalid = 0,
			/// <summary></summary>
			Character,
			/// <summary></summary>
			Word,
			/// <summary></summary>
			Phrase,
			/// <summary></summary>
			Sentence
		};

		/// <summary>
		/// Summary of field.
		/// </summary>
		public const int CONSTANT_FIELD = 5;

		/// <summary>
		/// Summary of field.
		/// </summary>
		public string NormalField;

		/// <summary>
		/// Summary of property.
		/// </summary>
		public bool NormalProperty { get; set; }

		/// <summary>
		/// Summary of when event is triggered.
		/// </summary>
		public event EventHandler NormalEvent;

		#region Constructors

		/// <summary>
		/// Summary of constructor.
		/// </summary>
		public NormalClass()
		{
		}

		/// <summary>
		/// Summary of constructor.
		/// </summary>
		/// <param name="text">Description of parameter.</param>
		public NormalClass(string text)
		{
			NormalField = text;
		}

		#endregion

		#region Static Methods

		/// <summary>
		/// Summary of method.
		/// </summary>
		public static void StaticMethodA()
		{
		}

		/// <summary>
		/// Summary of method.
		/// </summary>
		public static string StaticMethodB(int a, string b, DateTime c, List<string> d)
		{
			return "text";
		}

		#endregion

		#region Instance Methods

		/// <summary>
		/// Summary of method.
		/// </summary>
		public void InstanceMethodA()
		{
		}

		/// <summary>
		/// Summary of method.
		/// </summary>
		public string InstanceMethodB(int a, string b, DateTime c, List<string> d)
		{
			return "text";
		}

		#endregion

		#region Operators

		/// <summary>
		/// Summary of operator.
		/// </summary>
		/// <param name="a">Description of parameter a.</param>
		/// <param name="b">Description of parameter b.</param>
		/// <returns>Description of return value.</returns>
		public static NormalClass operator+(NormalClass a, NormalClass b)
		{
			return new NormalClass(a.NormalField + b.NormalField);
		}

		/// <summary></summary>
		public static NormalClass operator +(NormalClass a, string b)
		{
			return new NormalClass(a.NormalField + b);
		}

		/// <summary></summary>
		public static NormalClass operator +(string a, NormalClass b)
		{
			return new NormalClass(a + b.NormalField);
		}

		#endregion
	}
}
