using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Linq;
using WithoutHaste.DataFiles.DotNet;
using WithoutHaste.DataFiles.Markdown;

namespace EarlyDocs
{
	internal static class Ext
	{
		public const string MD = ".md";
	}

	class ConvertXML
	{
		public ConvertXML(string dllFilename, string xmlDocumentationFilename, string outputDirectory)
		{
			bool emptyOutputDirectoryFirst = true;

			DotNetDocumentationFile xmlDocumentation = new DotNetDocumentationFile(xmlDocumentationFilename);
			xmlDocumentation.AddAssemblyInfo(dllFilename);
			DotNetSettings.QualifiedNameConverter = DotNetSettings.DefaultQualifiedNameConverter;
			DotNetSettings.AdditionalQualifiedNameConverter = DotNetExtensions.QualifiedNameConverter;

			PrepareOutputDirectory(outputDirectory, emptyOutputDirectoryFirst);
			GenerateTypePages(xmlDocumentation.Types, outputDirectory);
			GenerateDelegatePages(xmlDocumentation.Delegates, outputDirectory);
			Save(GenerateTableOfContents(xmlDocumentation), outputDirectory, "TableOfContents" + Ext.MD);
		}

		private void PrepareOutputDirectory(string outputDirectory, bool emptyOutputDirectoryFirst)
		{
			if(!Directory.Exists(outputDirectory))
			{
				Directory.CreateDirectory(outputDirectory);
			}
			if(emptyOutputDirectoryFirst)
			{
				foreach(FileInfo file in (new DirectoryInfo(outputDirectory)).GetFiles())
				{
					file.Delete();
				}
			}
		}

		private void GenerateTypePages(List<DotNetType> types, string outputDirectory, string _namespace = null)
		{
			foreach(DotNetType type in types)
			{
				string typeName = type.Name.LocalName;
				if(!String.IsNullOrEmpty(_namespace))
					typeName = _namespace + "." + typeName;

				Save(type, outputDirectory, typeName + Ext.MD);

				GenerateTypePages(type.NestedTypes, outputDirectory, typeName);
				GenerateDelegatePages(type.Delegates, outputDirectory, typeName);
			}
		}

		private void GenerateDelegatePages(List<DotNetDelegate> delegates, string outputDirectory, string _namespace = null)
		{
			foreach(DotNetDelegate _delegate in delegates)
			{
				string delegateName = _delegate.Name.LocalName;
				if(!String.IsNullOrEmpty(_namespace))
					delegateName = _namespace + "." + delegateName;

				Save(_delegate, outputDirectory, delegateName + Ext.MD);
			}
		}

		private void Save(MarkdownFile markdown, string directory, string filename)
		{
			using(StreamWriter writer = new StreamWriter(Path.Combine(directory, filename)))
			{
				writer.Write(markdown.ToMarkdown());
			}
		}

		private void Save(DotNetType type, string directory, string filename)
		{
			//todo: move formatting of filename to central location that links can use
			filename = filename.Replace("<", "_").Replace(">", "_").Replace(",", "_");

			using(StreamWriter writer = new StreamWriter(Path.Combine(directory, filename)))
			{
				writer.Write(type.ToMarkdownFile().ToMarkdown());
			}
		}

		private void Save(DotNetDelegate _delegate, string directory, string filename)
		{
			//todo: move formatting of filename to central location that links can use
			filename = filename.Replace("<", "_").Replace(">", "_").Replace(",", "_");

			using(StreamWriter writer = new StreamWriter(Path.Combine(directory, filename)))
			{
				writer.Write(_delegate.ToMarkdownFile().ToMarkdown());
			}
		}

		private MarkdownFile GenerateTableOfContents(DotNetDocumentationFile xmlDocumentation)
		{
			MarkdownFile markdown = new MarkdownFile();
			MarkdownSection section = markdown.AddSection("Contents");
			AddTableOfContentsSection(section, "Concrete Types", xmlDocumentation.Types.Where(t => t.Category == TypeCategory.Normal).ToList());
			AddTableOfContentsSection(section, "Static Types", xmlDocumentation.Types.Where(t => t.Category == TypeCategory.Static).ToList());
			AddTableOfContentsSection(section, "Abstract Types", xmlDocumentation.Types.Where(t => t.Category == TypeCategory.Abstract).ToList());
			AddTableOfContentsSection(section, "Interfaces", xmlDocumentation.Types.Where(t => t.Category == TypeCategory.Interface).ToList());
			AddTableOfContentsSection(section, "Enums", xmlDocumentation.Types.Where(t => t.Category == TypeCategory.Enum).ToList());
			AddTableOfContentsSection(section, "Delegates", xmlDocumentation.Delegates);
			AddTableOfContentsSection(section, "Exceptions", xmlDocumentation.Types.Where(t => t.Category == TypeCategory.Exception).ToList());

			return markdown;
		}

		private void AddTableOfContentsSection(MarkdownSection parent, string header, List<DotNetType> types)
		{
			if(types.Count == 0) return;

			MarkdownSection section = parent.AddSection(header);
			foreach(DotNetType type in types.OrderBy(t => t.Name.LocalName))
			{
				section.AddInLine(new MarkdownInlineLink(type.Name.LocalName, type.Name.LocalName + Ext.MD));
				section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(type.SummaryComments));
				section.Add(new MarkdownLine());
			}
		}

		private void AddTableOfContentsSection(MarkdownSection parent, string header, List<DotNetDelegate> _delegates)
		{
			if(_delegates.Count == 0) return;

			MarkdownSection section = parent.AddSection(header);
			foreach(DotNetDelegate _delegate in _delegates.OrderBy(t => t.Name.LocalName))
			{
				section.AddInLine(new MarkdownInlineLink(_delegate.Name.LocalName, _delegate.Name.LocalName + Ext.MD));
				section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(_delegate.SummaryComments));
				section.Add(new MarkdownLine());
			}
		}
	}
}
