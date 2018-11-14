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

			//Assembly assembly = Assembly.LoadFrom(dllFilename); //for testing only

			PrepareOutputDirectory(outputDirectory, emptyOutputDirectoryFirst);
			BuildInternalFullNames(xmlDocumentation.Types);
			BuildInternalFullNames(xmlDocumentation.Delegates);
			BuildInterfaceImplementedByTypes(xmlDocumentation.Types);
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
			return "TableOfContents." + _namespace.FullName + Ext.MD;
		}

		private void GenerateTableOfContents(DotNetDocumentationFile xmlDocumentation, string directory)
		{
			List<DotNetQualifiedName> _namespaces = new List<DotNetQualifiedName>();
			foreach(DotNetType type in xmlDocumentation.Types)
			{
				_namespaces.Add(type.Name.FullNamespace);
			}
			foreach(DotNetDelegate _delegate in xmlDocumentation.Delegates)
			{
				_namespaces.Add(_delegate.Name.FullNamespace);
			}
			_namespaces = _namespaces.Distinct().ToList();

			foreach(DotNetQualifiedName _namespace in _namespaces)
			{
				DotNetQualifiedName parent = GetParentNamespace(_namespaces, _namespace);
				List<DotNetQualifiedName> childNamespaces = GetChildNamespaces(_namespaces, _namespace);
				Save(GenerateTableOfContents(xmlDocumentation, _namespace, parent, childNamespaces), directory, TableOfContentsFilename(_namespace));
			}
		}

		private MarkdownFile GenerateTableOfContents(DotNetDocumentationFile xmlDocumentation, DotNetQualifiedName _namespace, DotNetQualifiedName parent, List<DotNetQualifiedName> childNamespaces)
		{
			List<DotNetType> types = xmlDocumentation.Types.Where(t => t.Name.FullNamespace == _namespace).ToList();
			List<DotNetDelegate> _delegates = xmlDocumentation.Delegates.Where(d => d.Name.FullNamespace == _namespace).ToList();

			MarkdownFile markdown = new MarkdownFile();
			string header = "Contents of " + _namespace.FullName;
			if(parent != null)
				header = String.Format("Contents of [{0}]({1}).{2}", parent.FullName, TableOfContentsFilename(parent), _namespace.LocalName);

			MarkdownSection section = markdown.AddSection(header);

			if(childNamespaces != null && childNamespaces.Count > 0)
			{
				MarkdownSection childNamespacesSection = section.AddSection("Namespaces");
				foreach(DotNetQualifiedName childNamespace in childNamespaces)
				{
					section.AddInLine(new MarkdownInlineLink(childNamespace.FullName, TableOfContentsFilename(childNamespace)));
				}
			}

			AddTableOfContentsSection(section, "Concrete Types", types.Where(t => t.Category == TypeCategory.Normal).ToList());
			AddTableOfContentsSection(section, "Static Types", types.Where(t => t.Category == TypeCategory.Static).ToList());
			AddTableOfContentsSection(section, "Abstract Types", types.Where(t => t.Category == TypeCategory.Abstract).ToList());
			AddTableOfContentsSection(section, "Interfaces", types.Where(t => t.Category == TypeCategory.Interface).ToList());
			AddTableOfContentsSection(section, "Enums", types.Where(t => t.Category == TypeCategory.Enum).ToList());
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

		/// <summary>Returns the closest parent in the provided list, or null.</summary>
		private DotNetQualifiedName GetParentNamespace(List<DotNetQualifiedName> _namespaces, DotNetQualifiedName _namespace)
		{
			DotNetQualifiedName parent = null;
			foreach(DotNetQualifiedName other in _namespaces)
			{
				if(other == _namespace) continue;
				if(!_namespace.IsWithin(other)) continue;
				if(parent == null)
					parent = other;
				else if(other.IsWithin(parent))
					parent = other;
			}
			return parent;
		}

		private List<DotNetQualifiedName> GetChildNamespaces(List<DotNetQualifiedName> _namespaces, DotNetQualifiedName _namespace)
		{
			List<DotNetQualifiedName> childNamespaces = new List<DotNetQualifiedName>();
			foreach(DotNetQualifiedName other in _namespaces)
			{
				if(other == _namespace) continue;
				if(GetParentNamespace(_namespaces, other) == _namespace)
					childNamespaces.Add(other);
			}
			return childNamespaces;
		}
	}
}
