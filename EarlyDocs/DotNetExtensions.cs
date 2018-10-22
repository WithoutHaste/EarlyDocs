using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

			switch(name.FullName)
			{
				case "System.Double": return "double";
				case "System.Int16": return "short";
				case "System.Int32": return "int";
				case "System.Int64": return "long";
				case "System.Single": return "float";
				case "System.String": return "string";
			}
			switch(name.FullNamespace)
			{
				case "System.Collections.Generic": return name.LocalName;
				case "System": return name.LocalName;
			}
			return name.FullName;
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
			PreSummaryToMarkdown(typeSection, type);
			if(type.SummaryComments.Count > 0) //todo: these ifs are duplicated in DotNetMember ToMarkdownSection
			{
				typeSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(type.SummaryComments));
			}
			if(type.RemarksComments.Count > 0)
			{
				typeSection.Add(new MarkdownLine(MarkdownText.Bold("Remarks:")));
				typeSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(type.RemarksComments));
			}
			if(type.ExampleComments.Count > 0)
			{
				//todo: duplicate code with method examples
				MarkdownSection exampleSection = typeSection.AddSection("Examples");
				char index = 'A';
				foreach(DotNetComment comment in type.ExampleComments)
				{
					exampleSection.Add(new MarkdownLine(MarkdownText.Bold("Example " + index + ":")));
					exampleSection.Add(ConvertDotNet.DotNetCommentsToMarkdown(comment));
					index++; //todo: if more than 26 comments, loop to AA,AB,...
				}
			}
			if(type.NestedEnums.Count > 0)
			{
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
			
			if(type.ConstructorMethods.Count > 0)
				typeSection.Add(MethodsToMarkdown("Constructors", type.ConstructorMethods.Cast<DotNetMethod>().ToList()));
			if(type.StaticMethods.Count > 0)
				typeSection.Add(MethodsToMarkdown("Static Methods", type.StaticMethods));
			if(type.NormalMethods.Count > 0)
				typeSection.Add(MethodsToMarkdown("Methods", type.NormalMethods));
			if(type.OperatorMethods.Count > 0)
				typeSection.Add(MethodsToMarkdown("Operators", type.OperatorMethods.Cast<DotNetMethod>().ToList()));
			/* todo Nested Types
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

		public static MarkdownSection ToMarkdownSection(this DotNetField field)
		{
			MarkdownSection memberSection = new MarkdownSection(field.ToHeader());

			if(field.SummaryComments.Count > 0)
			{
				memberSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(field.SummaryComments));
			}
			if(field.RemarksComments.Count > 0)
			{
				memberSection.Add(new MarkdownLine(MarkdownText.Bold("Remarks:")));
				memberSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(field.RemarksComments));
			}
			if(field.ExampleComments.Count > 0)
			{
				char index = 'A'; //todo cleanup duplicated example sections
				foreach(DotNetComment comment in field.ExampleComments)
				{
					string exampleHeader = "Example " + index + ":";
					if(field.ExampleComments.Count == 1)
						exampleHeader = "Example:";
					memberSection.Add(new MarkdownLine(MarkdownText.Bold(exampleHeader)));
					memberSection.Add(ConvertDotNet.DotNetCommentsToMarkdown(comment));
					index++;
				}
			}
			foreach(DotNetCommentQualifiedLinkedGroup comment in field.PermissionComments)
			{
				string permissionHeader = "Permission: " + comment.QualifiedLink.Name.ToDisplayString(field.Name.FullNamespace);
				if(field.Name == comment.QualifiedLink.Name)
					permissionHeader = "Permission:";
				memberSection.Add(new MarkdownLine(MarkdownText.Bold(permissionHeader)));
				memberSection.Add(ConvertDotNet.DotNetCommentsToMarkdown(comment));
			}
			if(field.FloatingComments != null)
			{
				memberSection.Add(ConvertDotNet.DotNetCommentsToMarkdown(field.FloatingComments));
			}

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
			MarkdownSection memberSection = new MarkdownSection(header);

			if(method.SummaryComments.Count > 0)
			{
				memberSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(method.SummaryComments));
			}
			if(method.RemarksComments.Count > 0)
			{
				memberSection.Add(new MarkdownLine(MarkdownText.Bold("Remarks:")));
				memberSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(method.RemarksComments));
			}
			if(method.ParameterComments.Count > 0)
			{
				//todo: order parameters as they are ordered in method signature
				//todo: if ALL parameters are just simple one-line text, display more succinctly
				MarkdownSection parameterSection = memberSection.AddSection("Parameters");
				foreach(DotNetCommentParameter comment in method.ParameterComments)
				{
					parameterSection.AddSection(ToMarkdownSection(comment));
				}
			}
			if(method.ReturnsComments != null)
			{
				memberSection.Add(new MarkdownLine(MarkdownText.Bold("Returns:")));
				memberSection.Add(ConvertDotNet.DotNetCommentsToMarkdown(method.ReturnsComments));
			}
			if(method.ExampleComments.Count > 0)
			{
				char index = 'A'; //todo cleanup duplicated example sections
				foreach(DotNetComment comment in method.ExampleComments)
				{
					string exampleHeader = "Example " + index + ":";
					if(method.ExampleComments.Count == 1)
						exampleHeader = "Example:";
					memberSection.Add(new MarkdownLine(MarkdownText.Bold(exampleHeader)));
					memberSection.Add(ConvertDotNet.DotNetCommentsToMarkdown(comment));
					index++;
				}
			}
			foreach(DotNetCommentQualifiedLinkedGroup comment in method.PermissionComments)
			{
				string permissionHeader = "Permission: " + comment.QualifiedLink.Name.ToDisplayString(method.Name.FullNamespace);
				if(comment is DotNetCommentMethodLinkedGroup)
				{
					if(method.MatchesSignature((comment as DotNetCommentMethodLinkedGroup).MethodLink))
					{
						permissionHeader = "Permission: ";
					}
				}
				memberSection.Add(new MarkdownLine(MarkdownText.Bold(permissionHeader)));
				memberSection.Add(ConvertDotNet.DotNetCommentsToMarkdown(comment));
			}
			if(method.ExceptionComments.Count > 0)
			{
				memberSection.Add(new MarkdownLine(MarkdownText.Bold("Exceptions:")));
				foreach(DotNetCommentQualifiedLinkedGroup comment in method.ExceptionComments)
				{
					memberSection.Add(MarkdownText.Italic(comment.QualifiedLink.Name.FullName + ": "));
					memberSection.Add(ConvertDotNet.DotNetCommentsToMarkdown(comment));
				}
			}
			if(method.FloatingComments != null)
			{
				memberSection.Add(ConvertDotNet.DotNetCommentsToMarkdown(method.FloatingComments));
			}

			//todo: param/typeparam info
			//todo: returns

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

		private static void PreSummaryToMarkdown(MarkdownSection parent, DotNetType type)
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

		/*
		public static IMarkdownInSection ToMarkdown(this DotNetComment comment)
		{
			//should not be needed, since all subclasses are handled explicitly
			//but is required for compilation
			throw new NotImplementedException("Unknown comment type.");
		}

		public static MarkdownText ToMarkdown(this DotNetCommentCode code)
		{
			return new MarkdownText(code.Text);
		}

		public static MarkdownCodeBlock ToMarkdown(this DotNetCommentCodeBlock codeBlock)
		{
			return new MarkdownCodeBlock(codeBlock.Text, codeBlock.Language);
		}

		public static IMarkdownInSection[] ToMarkdown(this DotNetCommentGroup group)
		{
			return group.Comments.Select(x => x.ToMarkdown()).ToArray();
		}

		//public static IMarkdownInSection ToMarkdown(this DotNetCommentLinkedGroup<IDotNetCommentLink> group)
		//{
		//}

		public static MarkdownParagraph ToMarkdown(this DotNetCommentParameter parameter)
		{
			return new MarkdownParagraph(String.Format("Parameter **{0}**: {1}", parameter.Link.Name, "TODO"));
		}

		public static MarkdownTable ToMarkdown(this DotNetCommentTable table)
		{
		}

		public static MarkdownList ToMarkdown(this DotNetCommentList list)
		{
			MarkdownList mdList = new MarkdownList(list.IsNumbered);

			foreach(DotNetCommentListItem item in list.Items)
			{
				mdList.Add(item.ToMarkdown());
			}

			return mdList;
		}

		public static IMarkdownInList ToMarkdown(this DotNetCommentListItem item)
		{
			if(item.IsHeader)
			{
				if(String.IsNullOrEmpty(item.Description))
					return new MarkdownLine("**" + item.Term + "**");
				return new MarkdownLine(String.Format("**{0}: {1}**", item.Term, item.Description));
			}
			else
			{
				if(String.IsNullOrEmpty(item.Description))
					return new MarkdownLine(item.Term);
				return new MarkdownLine(String.Format("{0}: {1}", item.Term, item.Description));
			}
		}*/
	}
}
