using System;
using System.Collections.Generic;
using System.Text;

namespace Test
{
	/// <summary>
	/// Tests include code and xml in comments.
	/// </summary>
	public class ClassCode
	{
		/// <summary>
		///		[Summary Tag]
		///		<code>
		///		int a = 0;
		///		int b = 1;
		///		</code>
		/// </summary>
		/// <code>
		/// string c = "c";
		/// string d = "d";
		/// </code>
		public int CodeBlock = 0;

		/// <summary>
		/// Word word <c>int a = 0;</c> word word word.
		/// </summary>
		/// <remarks>Code including backtics: <c>a`aa``a`</c></remarks>
		/// Misc misc misc <c>public static void Main(string[] args) { }</c> misc misc misc.
		public int InlineCode = 0;

		/// <summary>
		///		[Summary Tag]
		///		<code lang='php'>
		///		<![CDATA[
		///		<?php
		///		$txt = "Hello world!";
		///		$x = 5;
		///		$y = 10.5;
		///		
		///		echo $txt;
		///		?>
		///		]]>
		///		</code>
		/// </summary>
		/// <code lang='js'>
		/// function myTest() {
		///		document.getElementById('demo').style.fontSize='35px';
		///	}
		/// </code>
		public int CodeBlockWithLanguage = 0;

		/// <summary>
		/// Word word word <![CDATA[<html><body></body></html>]]> word word word.
		/// </summary>
		/// <example>
		///		<![CDATA[
		///		<!DOCTYPE html>
		///		<html>
		///		<body>
		///		</body>
		///		</html>
		///		]]>
		/// </example>
		public int InlineXml = 0;
	}
}
