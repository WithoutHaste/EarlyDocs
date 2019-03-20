using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using EarlyDocs;
using WithoutHaste.DataFiles.DotNet;

namespace EarlyDocsTest
{
	[TestClass]
	public class MiscTests
	{
		[TestMethod]
		public void MiscTests_GenericParameters()
		{
			//arrange
			//act
			DotNetDocumentationFile file = EarlyDocs.ConvertXML.LoadXmlAndAssembly("../../../Test/bin/Debug/Test.dll", "../../../Test/bin/Debug/Test.XML", new string[] { });
			DotNetType genericType = file.Types.First(t => t.Name.LocalName == "ClassExceptionGeneric<Apple>");
			string markdown = EarlyDocs.DotNetExtensions.ToMarkdownFile(genericType).ToMarkdownString();
			//assert
			Assert.IsTrue(markdown.Contains("Apple { public get; protected set; }"));
			Assert.IsTrue(markdown.Contains("ClassExceptionGeneric&lt;Apple&gt;(Apple value)"));
			Assert.IsTrue(markdown.Contains("MethodA(Apple value)"));
		}
	}
}
