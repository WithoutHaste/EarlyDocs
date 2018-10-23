using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WithoutHaste.DataFiles;
using WithoutHaste.DataFiles.DotNet;
using WithoutHaste.DataFiles.Markdown;

namespace EarlyDocs
{
	public static class DotNetExtensions
	{
		public static string ToHeader(this DotNetField field)
		{
			if(field is DotNetProperty)
				return ToHeader(field as DotNetProperty);
			return field.TypeName.ToDisplayString() + " " + field.Name.LocalName;
		}

		public static string ToHeader(this DotNetProperty property)
		{
			if(property is DotNetIndexer)
				return ToHeader(property as DotNetIndexer);
			string header = property.TypeName.ToDisplayString() + " " + property.Name.LocalName;
			if(property.Category == FieldCategory.Abstract)
				header = "abstract " + header;
			return header;
		}

		public static string ToHeader(this DotNetIndexer indexer)
		{
			return indexer.TypeName.ToDisplayString() + " this[" + String.Join(",", indexer.Parameters.Select(p => p.TypeName.ToDisplayString()).ToArray()) + "]";
		}

		public static string ToDisplayString(this DotNetParameter parameter)
		{
			if(parameter.TypeName == null)
				return parameter.Name;
			if(String.IsNullOrEmpty(parameter.Name))
				return parameter.TypeName.ToDisplayString();
			return parameter.TypeName.ToDisplayString() + " " + parameter.Name;
		}

		public static string ToDisplayString(this DotNetQualifiedName name, string _namespace)
		{
			_namespace += ".";

			string displayString = name.ToDisplayString();
			if(displayString.StartsWith(_namespace))
			{
				displayString = displayString.Substring(_namespace.Length);
			}
			return displayString;
		}

		public static string ToDisplayString(this DotNetQualifiedName name)
		{
			if(name == null)
				return "";
			return name.FullName;
		}

		public static string QualifiedNameConverter(string fullName, int depth)
		{
			if(depth > 0)
				return fullName;

			string[] commonNamespaces = new string[] { "System.", "System.Collections.Generic." };
			foreach(string _namespace in commonNamespaces)
			{
				if(!fullName.StartsWith(_namespace)) continue;

				string localName = fullName.RemoveFromStart(_namespace);
				if(localName.SplitIgnoreNested('.').Length > 1) continue;

				return localName;
			}

			return fullName;
		}

		public static MarkdownFile ToMarkdownFile(this DotNetType type)
		{
			MarkdownFile markdown = new MarkdownFile();
			if(type.Category == TypeCategory.Enum)
			{
				markdown.AddSection(type.ToMarkdownEnumSection());
			}
			else
			{
				markdown.AddSection(type.ToMarkdownSection());
			}

			return markdown;
		}

		public static MarkdownSection ToMarkdownSection(this DotNetType type)
		{
			MarkdownSection typeSection = new MarkdownSection(type.Name.LocalName);
			AddPreSummary(typeSection, type);
			AddSummary(typeSection, type as DotNetMember);
			AddRemarks(typeSection, type as DotNetMember);
			AddExamples(typeSection, type as DotNetMember);
			AddFloatingComments(typeSection, type as DotNetMember);
			if(type.NestedEnums.Count > 0)
			{
				//todo: simply list the enum as link to enum page, and list just the enum values without any comments as short-reference
				MarkdownSection enumSection = new MarkdownSection("Enums");
				typeSection.Add(enumSection);
				foreach(DotNetType e in type.NestedEnums.OrderBy(m => m.Name.LocalName))
				{
					enumSection.AddSection(e.ToMarkdownEnumSection());
				}
			}
			if(type.Fields.Count > 0)
			{
				MarkdownSection fieldSection = new MarkdownSection("Fields");
				typeSection.Add(fieldSection);
				if(type.ConstantFields.Count > 0)
				{
					foreach(DotNetField field in type.ConstantFields.OrderBy(m => m.Name.LocalName))
					{
						fieldSection.AddSection(ToMarkdownSection(field));
					}
				}
				if(type.NormalFields.Count > 0)
				{
					foreach(DotNetField field in type.NormalFields.OrderBy(m => m.Name.LocalName))
					{
						fieldSection.AddSection(ToMarkdownSection(field));
					}
				}
			}
			if(type.Properties.Count > 0)
			{
				MarkdownSection propertySection = new MarkdownSection("Properties");
				typeSection.Add(propertySection);
				if(type.IndexerProperties.Count > 0)
				{
					foreach(DotNetIndexer i in type.IndexerProperties.OrderBy(m => m.Parameters.Count).ThenBy(m => m.ParameterTypesSignature))
					{
						propertySection.AddSection(ToMarkdownSection(i as DotNetField));
					}
				}
				if(type.NormalProperties.Count > 0)
				{
					foreach(DotNetProperty p in type.NormalProperties.OrderBy(m => m.Name.LocalName))
					{
						propertySection.AddSection(ToMarkdownSection(p as DotNetField));
					}
				}
			}
			if(type.Events.Count > 0)
			{
				MarkdownSection eventSection = new MarkdownSection("Events");
				typeSection.Add(eventSection);
				foreach(DotNetEvent e in type.Events.OrderBy(m => m.Name.LocalName))
				{
					eventSection.AddSection(ToMarkdownSection(e as DotNetField));
				}
			}
			
			//todo: static constructors
			if(type.ConstructorMethods.Count > 0)
				typeSection.Add(MethodsToMarkdown("Constructors", type.ConstructorMethods.Cast<DotNetMethod>().ToList()));
			if(type.NormalMethods.Count > 0)
				typeSection.Add(MethodsToMarkdown("Methods", type.NormalMethods));
			if(type.StaticMethods.Count > 0)
				typeSection.Add(MethodsToMarkdown("Static Methods", type.StaticMethods));
			if(type.OperatorMethods.Count > 0)
				typeSection.Add(MethodsToMarkdown("Operators", type.OperatorMethods.Cast<DotNetMethod>().ToList()));
			//todo: destructors

			/* todo Nested Types: just a list of the type names with their summaries, linked to the type pages
			if(NestedTypes.Count > 0)
			{
				MarkdownSection nestedTypeSection = typeSection.AddSection("Nested Types");
				foreach(XmlType e in NestedTypes.OrderBy(m => m.Name))
				{
					nestedTypeSection.AddSection(e.ToMarkdownSection());
				}
			}*/

			return typeSection;
		}

		public static MarkdownFile ToMarkdownFile(this DotNetDelegate _delegate)
		{
			MarkdownFile markdown = new MarkdownFile();

			markdown.AddSection(ToMarkdownSection(_delegate as DotNetMethod));

			return markdown;
		}

		public static MarkdownSection ToMarkdownSection(this DotNetField field)
		{
			MarkdownSection memberSection = new MarkdownSection(field.ToHeader());

			AddSummary(memberSection, field as DotNetMember);
			AddRemarks(memberSection, field as DotNetMember);
			AddExamples(memberSection, field as DotNetMember);
			AddPermissions(memberSection, field as DotNetMember);
			AddFloatingComments(memberSection, field as DotNetMember);

			return memberSection;
		}

		public static MarkdownSection ToMarkdownSection(this DotNetMethod method)
		{
			string header = method.ReturnTypeName.ToDisplayString() + " " + method.Name.LocalName;
			if(method.Parameters == null || method.Parameters.Count == 0)
				header += "()";
			else
				header += String.Format("({0})", String.Join(", ", method.Parameters.Select(p => p.ToDisplayString()).ToArray()));
			if(method.Category == MethodCategory.Abstract)
				header = "abstract " + header;

			string fullHeader = header;
			if(method is DotNetDelegate)
			{
				header = method.Name.LocalName;
			}

			MarkdownSection memberSection = new MarkdownSection(header);

			if(method is DotNetDelegate)
			{
				memberSection.AddInLine(MarkdownText.Bold("Delegate"));
				memberSection.AddInParagraph(MarkdownText.Bold(fullHeader));
			}

			AddSummary(memberSection, method as DotNetMember);
			AddRemarks(memberSection, method as DotNetMember);
			if(method.ParameterComments.Count > 0)
			{
				//todo: order parameters as they are ordered in method signature
				//todo: if ALL parameters are just simple one-line text, display more succinctly
				MarkdownSection parameterSection = memberSection.AddSection("Parameters");
				foreach(DotNetCommentParameter comment in method.ParameterComments)
				{
					parameterSection.AddSection(ToMarkdownSection(comment));
				}
				memberSection.Add(new MarkdownLine());
			}
			AddReturns(memberSection, method as DotNetMember);
			AddExamples(memberSection, method as DotNetMember);
			AddPermissions(memberSection, method as DotNetMember);
			AddExceptions(memberSection, method as DotNetMember);
			AddFloatingComments(memberSection, method as DotNetMember);

			return memberSection;
		}

		public static MarkdownSection ToMarkdownSection(this DotNetCommentParameter parameter)
		{
			string header = parameter.ParameterLink.Name;

			MarkdownSection section = new MarkdownSection(header);
			section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(parameter));
			return section;
		}

		public static MarkdownSection ToMarkdownEnumSection(this DotNetType type)
		{
			if(type.Category != TypeCategory.Enum)
				throw new Exception("Intended for enums only.");

			MarkdownSection enumSection = new MarkdownSection(type.Name.LocalName);

			if(type.SummaryComments.Count > 0) enumSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(type.SummaryComments));
			if(type.RemarksComments.Count > 0) enumSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(type.RemarksComments));
			if(type.ExampleComments.Count > 0)
			{
				MarkdownSection exampleSection = enumSection.AddSection("Examples");
				exampleSection.Add(ConvertDotNet.DotNetCommentsToMarkdown(type.ExampleComments));
			}
			if(type.Fields.Count > 0)
			{
				//todo: given the various documentation options here, should probably do each value as a whole section instead of as a list item
				MarkdownSection fieldSection = enumSection.AddSection("Constants");
				MarkdownList list = new MarkdownList(isNumbered: false);
				fieldSection.Add(list);

				foreach(DotNetField field in type.Fields)
				{
					//todo: default to using Summary or Value tag, instead of just Summary
					//todo: if a field here has more than just a one-line description, this list format won't work
					if(field.SummaryComments.Count == 0)
					{
						list.Add(new MarkdownText(field.Name));
					}
					else
					{
						//todo: support multiple summary tags - maybe in DataFiles it should be appending all the summary tag contents together? so only one unit is presented to this side? probably this
						list.Add(new MarkdownText(String.Format("{0}: {1}", field.Name, "TODO")));
						//todo: support examples
						/*
						if(field.Examples.Count > 0)
						{
							MarkdownList exampleList = new MarkdownList();
							list.Add(exampleList);
							foreach(XmlComments example in field.Examples)
							{
								exampleList.Add(new MarkdownText("Example: " + example.ToMarkdown()));
							}
						}
						*/
					}
				}
			}
			return enumSection;
		}

		private static void AddPreSummary(MarkdownSection parent, DotNetType type)
		{
			bool changeMade = false;

			//todo: make bold
			switch(type.Category)
			{
				case TypeCategory.Static: parent.Add(new MarkdownLine(MarkdownText.Bold("Static"))); changeMade = true; break;
				case TypeCategory.Interface: parent.Add(new MarkdownLine(MarkdownText.Bold("Interface"))); changeMade = true; break;
				case TypeCategory.Abstract: parent.Add(new MarkdownLine(MarkdownText.Bold("Abstract"))); changeMade = true; break;
				case TypeCategory.Enum: parent.Add(new MarkdownLine(MarkdownText.Bold("Enumeration"))); changeMade = true; break;
			}

			if(type.BaseType != null)
			{
				MarkdownLine inheritanceLine = new MarkdownLine(type.BaseType.Name.FullName); //todo: shorten in-project names to LocalName and make them links to documentation
				DotNetBaseType baseType = type.BaseType.BaseType;
				while(baseType != null)
				{
					inheritanceLine.Prepend(baseType.Name.FullName + " → ");
					baseType = baseType.BaseType;
				}
				inheritanceLine.Prepend(" ");
				inheritanceLine.Prepend(MarkdownText.Bold("Inheritance:"));
				parent.Add(inheritanceLine);
				changeMade = true;
			}

			if(type.ImplementedInterfaces.Count > 0)
			{
				MarkdownLine interfaceLine = new MarkdownLine(MarkdownText.Bold("Implements:"), new MarkdownText(" "));
				interfaceLine.Add(String.Join(", ", type.ImplementedInterfaces.Select(i => i.Name.FullName).ToArray())); //todo: shorten in-project names to LocalName and make them links to documentation
				parent.Add(interfaceLine);
				changeMade = true;
			}

			if(changeMade)
			{
				parent.Add(new MarkdownLine());
			}
		}

		private static MarkdownSection MethodsToMarkdown(string header, List<DotNetMethod> methods)
		{
			MarkdownSection methodSection = new MarkdownSection(header);
			methods = methods.OrderBy(m => m.Name.LocalName).ToList();
			foreach(DotNetMethod method in methods)
			{
				methodSection.AddSection(ToMarkdownSection(method));
			}
			return methodSection;
		}

		private static void AddSummary(MarkdownSection section, DotNetMember member)
		{
			if(member.SummaryComments.Count == 0)
				return;

			section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(member.SummaryComments));
			section.Add(new MarkdownLine());
		}

		private static void AddRemarks(MarkdownSection section, DotNetMember member)
		{
			if(member.RemarksComments.Count == 0)
				return;

			section.Add(new MarkdownLine(MarkdownText.Bold("Remarks:")));
			section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(member.RemarksComments));
			section.Add(new MarkdownLine());
		}

		private static void AddReturns(MarkdownSection section, DotNetMember member)
		{
			if(member.ReturnsComments.IsEmpty)
				return;

			section.Add(new MarkdownLine(MarkdownText.Bold("Returns:")));
			section.Add(ConvertDotNet.DotNetCommentsToMarkdown(member.ReturnsComments));
			section.Add(new MarkdownLine());
		}

		private static void AddExamples(MarkdownSection section, DotNetMember member)
		{
			if(member.ExampleComments.Count == 0)
				return;

			char index = 'A'; //todo: update indexer to go to "AA", "AB" after "Z"
			foreach(DotNetComment comment in member.ExampleComments)
			{
				string exampleHeader = "Example " + index + ":";
				if(member.ExampleComments.Count == 1)
					exampleHeader = "Example:";
				section.Add(new MarkdownLine(MarkdownText.Bold(exampleHeader)));
				section.Add(ConvertDotNet.DotNetCommentsToMarkdown(comment));
				index++;
			}
			section.Add(new MarkdownLine());
		}

		private static void AddPermissions(MarkdownSection section, DotNetMember member)
		{
			foreach(DotNetCommentQualifiedLinkedGroup comment in member.PermissionComments)
			{
				string permissionHeader = "Permission: " + comment.QualifiedLink.Name.ToDisplayString(member.Name.FullNamespace);
				if(member.Name == comment.QualifiedLink.Name)
				{
					permissionHeader = "Permission:";
				}
				if(comment is DotNetCommentMethodLinkedGroup && member is DotNetMethod)
				{
					if((member as DotNetMethod).MatchesSignature((comment as DotNetCommentMethodLinkedGroup).MethodLink))
					{
						permissionHeader = "Permission:";
					}
				}
				section.Add(new MarkdownLine(MarkdownText.Bold(permissionHeader)));
				section.Add(ConvertDotNet.DotNetCommentsToMarkdown(comment));
				section.Add(new MarkdownLine());
			}
		}

		private static void AddExceptions(MarkdownSection section, DotNetMember member)
		{
			if(member.ExceptionComments.Count == 0)
				return;

			section.Add(new MarkdownLine(MarkdownText.Bold("Exceptions:")));
			foreach(DotNetCommentQualifiedLinkedGroup comment in member.ExceptionComments)
			{
				section.Add(MarkdownText.Italic(comment.QualifiedLink.Name.FullName + ":"), new MarkdownText(" "));
				section.Add(ConvertDotNet.DotNetCommentsToMarkdown(comment));
			}
			section.Add(new MarkdownLine());
		}

		private static void AddFloatingComments(MarkdownSection section, DotNetMember member)
		{
			if(member.FloatingComments.IsEmpty)
				return;

			section.Add(ConvertDotNet.DotNetCommentsToMarkdown(member.FloatingComments));
		}

	}
}
