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
			DotNetExtensions.TurnQualifiedNameConverterOn();

			PrepareOutputDirectory(outputDirectory, emptyOutputDirectoryFirst);
			BuildInternalFullNames(xmlDocumentation.Types);
			BuildInternalFullNames(xmlDocumentation.Delegates);
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

		private void BuildInternalFullNames(List<DotNetType> types)
		{
			foreach(DotNetType type in types)
			{
				string typeName = type.Name.FullName;
				DotNetExtensions.InternalFullNames.Add(typeName);
				DotNetExtensions.InternalFullNames.Add(typeName.Replace("<", "&lt;").Replace(">", "&gt;"));

				BuildInternalFullNames(type.NestedTypes);
				BuildInternalFullNames(type.Delegates);
			}
		}

		private void BuildInternalFullNames(List<DotNetDelegate> delegates)
		{
			foreach(DotNetDelegate _delegate in delegates)
			{
				string delegateName = _delegate.Name.FullName;
				DotNetExtensions.InternalFullNames.Add(delegateName);
				DotNetExtensions.InternalFullNames.Add(delegateName.Replace("<", "&lt;").Replace(">", "&gt;"));
			}
		}

		private void GenerateTypePages(List<DotNetType> types, string outputDirectory)
		{
			foreach(DotNetType type in types)
			{
				string typeName = type.Name.FullName;
				Save(type, outputDirectory, typeName + Ext.MD);
				DotNetExtensions.InternalFullNames.Add(typeName);

				GenerateTypePages(type.NestedTypes, outputDirectory);
				GenerateDelegatePages(type.Delegates, outputDirectory);
			}
		}

		private void GenerateDelegatePages(List<DotNetDelegate> delegates, string outputDirectory)
		{
			foreach(DotNetDelegate _delegate in delegates)
			{
				string delegateName = _delegate.Name.FullName;
				Save(_delegate, outputDirectory, delegateName + Ext.MD);
				DotNetExtensions.InternalFullNames.Add(delegateName);
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
				section.AddInLine(new MarkdownInlineLink(type.Name.LocalName, type.Name.FullName + Ext.MD));
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
				section.AddInLine(new MarkdownInlineLink(_delegate.Name.LocalName, _delegate.Name.FullName + Ext.MD));
				section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(_delegate.SummaryComments));
				section.Add(new MarkdownLine());
			}
		}
	}
}
