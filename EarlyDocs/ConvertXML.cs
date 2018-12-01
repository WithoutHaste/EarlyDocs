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

	internal class ConvertXML
	{
		internal ConvertXML(string dllFilename, string xmlDocumentationFilename, string outputDirectory, string[] includeDlls, bool emptyOutputDirectoryFirst)
		{
			DotNetDocumentationFile xmlDocumentation = new DotNetDocumentationFile(xmlDocumentationFilename);
			xmlDocumentation.AddAssemblyInfo(dllFilename, includeDlls);
			DotNetExtensions.TurnQualifiedNameConverterOn();

			PrepareOutputDirectory(outputDirectory, emptyOutputDirectoryFirst);
			BuildInternalFullNames(xmlDocumentation.Types);
			BuildInternalFullNames(xmlDocumentation.Delegates);
			BuildInterfaceImplementedByTypes(xmlDocumentation.Types);
			BuildTypeDerivedBy(xmlDocumentation.Types);
			GenerateTypePages(xmlDocumentation.Types, outputDirectory);
			GenerateDelegatePages(xmlDocumentation.Delegates, outputDirectory);
			GenerateTableOfContents(xmlDocumentation, outputDirectory);
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

		private void BuildInterfaceImplementedByTypes(List<DotNetType> types)
		{
			foreach(DotNetType type in types)
			{
				foreach(DotNetBaseType implementedInterface in type.ImplementedInterfaces)
				{
					if(!DotNetExtensions.InterfaceImplementedByTypes.ContainsKey(implementedInterface.Name))
						DotNetExtensions.InterfaceImplementedByTypes[implementedInterface.Name] = new List<DotNetType>();
					DotNetExtensions.InterfaceImplementedByTypes[implementedInterface.Name].Add(type);
				}
				BuildInterfaceImplementedByTypes(type.NestedTypes);
			}
		}

		private void BuildTypeDerivedBy(List<DotNetType> types)
		{
			foreach(DotNetType type in types)
			{
				if(type.BaseType != null)
				{
					if(!DotNetExtensions.TypeDerivedBy.ContainsKey(type.BaseType.Name))
						DotNetExtensions.TypeDerivedBy[type.BaseType.Name] = new List<DotNetType>();
					DotNetExtensions.TypeDerivedBy[type.BaseType.Name].Add(type);
				}
				BuildTypeDerivedBy(type.NestedTypes);
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

		internal static string FormatFilename(string filename)
		{
			//for generic type parameters
			filename = filename.Replace("<", "_").Replace(">", "_").Replace(",", "_");
			return filename;
		}

		private void Save(DotNetType type, string directory, string filename)
		{
			using(StreamWriter writer = new StreamWriter(Path.Combine(directory, FormatFilename(filename))))
			{
				writer.Write(type.ToMarkdownFile().ToMarkdown());
			}
		}

		private void Save(DotNetDelegate _delegate, string directory, string filename)
		{
			using(StreamWriter writer = new StreamWriter(Path.Combine(directory, FormatFilename(filename))))
			{
				writer.Write(_delegate.ToMarkdownFile().ToMarkdown());
			}
		}

		internal static string TableOfContentsFilename(DotNetQualifiedName _namespace)
		{
			return "TableOfContents." + FormatFilename(_namespace.FullName) + Ext.MD;
		}

		private void GenerateTableOfContents(DotNetDocumentationFile xmlDocumentation, string directory)
		{
			List<DotNetQualifiedClassName> _namespaces = new List<DotNetQualifiedClassName>();
			foreach(DotNetType type in xmlDocumentation.Types)
			{
				_namespaces.Add(type.TypeName.FullClassNamespace);
			}
			foreach(DotNetDelegate _delegate in xmlDocumentation.Delegates)
			{
				_namespaces.Add(_delegate.MethodName.FullClassNamespace);
			}
			_namespaces = _namespaces.Distinct().ToList();
			DotNetQualifiedClassNameTreeNode root = DotNetQualifiedClassNameTreeNode.Generate(_namespaces);
			GenerateAndSaveTableOfContents(xmlDocumentation, root, directory);

			//master table of contents
			MarkdownFile markdown = new MarkdownFile();
			MarkdownSection section = markdown.AddSection("Table of Contents");
			if(root.Value == null)
			{
				foreach(DotNetQualifiedClassNameTreeNode node in root.Children)
				{
					MarkdownSection subsection = BuildMasterSummary(xmlDocumentation, node);
					section.AddSection(subsection);
				}
			}
			else
			{
				MarkdownSection subsection = BuildMasterSummary(xmlDocumentation, root);
				section.AddSection(subsection);
			}
			Save(markdown, directory, "TableOfContents" + Ext.MD);
		}

		private MarkdownSection BuildMasterSummary(DotNetDocumentationFile xmlDocumentation, DotNetQualifiedClassNameTreeNode node)
		{
			string header= (new MarkdownInlineLink(node.Value, TableOfContentsFilename(node.Value))).ToMarkdown(null);
			MarkdownSection section = new MarkdownSection(header);
			List<DotNetType> types = xmlDocumentation.Types.Where(t => t.Name.FullNamespace == node.Value).ToList();
			foreach(DotNetType type in types.OrderBy(t => t.Name))
			{
				section.Add(new MarkdownLine(new MarkdownInlineLink(type.Name, FormatFilename(type.Name + Ext.MD))));
			}
			List<DotNetDelegate> _delegates = xmlDocumentation.Delegates.Where(d => d.Name.FullNamespace == node.Value).ToList();
			foreach(DotNetDelegate _delegate in _delegates.OrderBy(d => d.Name))
			{
				section.Add(new MarkdownLine(new MarkdownInlineLink(_delegate.Name, FormatFilename(_delegate.Name + Ext.MD))));
			}
			foreach(DotNetQualifiedClassNameTreeNode child in node.Children.OrderBy(c => c.Value))
			{
				section.AddSection(BuildMasterSummary(xmlDocumentation, child));
			}
			return section;
		}

		private void GenerateAndSaveTableOfContents(DotNetDocumentationFile xmlDocumentation, DotNetQualifiedClassNameTreeNode node, string directory)
		{
			if(node.Value != null)
				Save(GenerateTableOfContents(xmlDocumentation, node), directory, TableOfContentsFilename(node.Value));
			foreach(DotNetQualifiedClassNameTreeNode child in node.Children)
			{
				GenerateAndSaveTableOfContents(xmlDocumentation, child, directory);
			}
		}

		private MarkdownFile GenerateTableOfContents(DotNetDocumentationFile xmlDocumentation, DotNetQualifiedClassNameTreeNode node)
		{
			List<DotNetType> types = xmlDocumentation.Types.Where(t => t.Name.FullNamespace == node.Value).ToList();
			List<DotNetDelegate> _delegates = xmlDocumentation.Delegates.Where(d => d.Name.FullNamespace == node.Value).ToList();

			MarkdownFile markdown = new MarkdownFile();
			string header = "Contents of " + node.Value.FullName;
			if(node.Parent != null)
				header = String.Format("Contents of [{0}]({1}).{2}", node.Parent.Value.FullName, TableOfContentsFilename(node.Parent.Value), node.Value.LocalName);

			MarkdownSection section = markdown.AddSection(header);

			if(node.Children.Count > 0)
			{
				MarkdownSection childNamespacesSection = section.AddSection("Namespaces");
				foreach(DotNetQualifiedClassNameTreeNode child in node.Children)
				{
					section.AddInLine(new MarkdownInlineLink(MarkdownText.Bold(child.Value.FullName), TableOfContentsFilename(child.Value)));
				}
			}

			AddTableOfContentsSection(section, "Concrete Types", types.Where(t => t.Category == TypeCategory.Normal).ToList());
			AddTableOfContentsSection(section, "Static Types", types.Where(t => t.Category == TypeCategory.Static).ToList());
			AddTableOfContentsSection(section, "Abstract Types", types.Where(t => t.Category == TypeCategory.Abstract).ToList());
			AddTableOfContentsSection(section, "Interfaces", types.Where(t => t.Category == TypeCategory.Interface).ToList());
			AddTableOfContentsSection(section, "Enums", types.Where(t => t.Category == TypeCategory.Enum).ToList());
			AddTableOfContentsSection(section, "Structs", types.Where(t => t.Category == TypeCategory.Struct).ToList());
			AddTableOfContentsSection(section, "Delegates", _delegates);
			AddTableOfContentsSection(section, "Exceptions", types.Where(t => t.Category == TypeCategory.Exception).ToList());

			return markdown;
		}

		private void AddTableOfContentsSection(MarkdownSection parent, string header, List<DotNetType> types)
		{
			if(types.Count == 0) return;

			MarkdownSection section = parent.AddSection(header);
			foreach(DotNetType type in types.OrderBy(t => t.Name.LocalName))
			{
				section.AddInLine(new MarkdownInlineLink(MarkdownText.Bold(type.Name.LocalName), type.Name.FullName + Ext.MD));
				section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(type.SummaryComments, type));
				section.Add(new MarkdownLine());
			}
		}

		private void AddTableOfContentsSection(MarkdownSection parent, string header, List<DotNetDelegate> _delegates)
		{
			if(_delegates.Count == 0) return;

			MarkdownSection section = parent.AddSection(header);
			foreach(DotNetDelegate _delegate in _delegates.OrderBy(t => t.Name.LocalName))
			{
				section.AddInLine(new MarkdownInlineLink(MarkdownText.Bold(_delegate.Name.LocalName), _delegate.Name.FullName + Ext.MD));
				section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(_delegate.SummaryComments, _delegate));
				section.Add(new MarkdownLine());
			}
		}
	}
}
