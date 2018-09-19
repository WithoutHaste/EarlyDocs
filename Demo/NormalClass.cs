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
	public class NormalClass
	{
	}
}
