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
		/// <summary>
		/// List of all full names of known types/delegates in the assembly being documented
		/// </summary>
		public static List<string> InternalFullNames = new List<string>();

		public static List<DotNetQualifiedName> KnownMicrosoftNamespaces = new List<DotNetQualifiedName>() {
			DotNetQualifiedName.FromVisualStudioXml("System"),
			DotNetQualifiedName.FromVisualStudioXml("System.Collections.Generic"),
			DotNetQualifiedName.FromVisualStudioXml("System.Linq"),
			DotNetQualifiedName.FromVisualStudioXml("System.Text"),
			DotNetQualifiedName.FromVisualStudioXml("System.Threading.Tasks")
		};

		public static bool IsInKnownMicrosoftNamespace(this DotNetQualifiedName name)
		{
			if(KnownMicrosoftNamespaces.Contains(name))
				return true;
			if(name.FullNamespace != null)
			{
				if(name.FullNamespace.IsInKnownMicrosoftNamespace())
					return true;
			}
			return false;
		}

		public static string ToHeader(this DotNetField field, DotNetType parent)
		{
			if(field is DotNetProperty)
				return ToHeader(field as DotNetProperty, parent);

			string header = field.TypeName.ToDisplayStringLink(parent.Name.FullName) + " " + field.Name.LocalName;

			switch(field.Category)
			{
				case FieldCategory.Constant: header = "const " + header; break;
				case FieldCategory.ReadOnly: header = "readonly " + header; break;
			}

			switch(field.AccessModifier)
			{
				case AccessModifier.Protected: header = "protected " + header; break;
				case AccessModifier.Internal: header = "internal " + header; break;
				case AccessModifier.InternalProtected: header = "internal protected " + header; break;
				case AccessModifier.Private: header = "private " + header; break;
			}

			if(field.IsStatic && field.Category != FieldCategory.Constant)
				header = "static " + header;

			return header;
		}

		public static string ToHeader(this DotNetProperty property, DotNetType parent)
		{
			if(property is DotNetIndexer)
				return ToHeader(property as DotNetIndexer);

			string header = property.TypeName.ToDisplayStringLink(parent.Name.FullName) + " " + property.Name.LocalName;

			if(property.Category == FieldCategory.Abstract)
				header = "abstract " + header;

			header += " { ";
			if(property.HasGetterMethod)
			{
				switch(property.GetterMethod.AccessModifier)
				{
					case AccessModifier.Public: header += "get; "; break;
					case AccessModifier.Protected: header += "protected get; "; break;
					case AccessModifier.Internal: header += "internal get; "; break;
					case AccessModifier.InternalProtected: header += "internal protected get; "; break;
					case AccessModifier.Private: header += "private get; "; break;
				}
			}
			if(property.HasSetterMethod)
			{
				switch(property.SetterMethod.AccessModifier)
				{
					case AccessModifier.Public: header += "set; "; break;
					case AccessModifier.Protected: header += "protected set; "; break;
					case AccessModifier.Internal: header += "internal set; "; break;
					case AccessModifier.InternalProtected: header += "internal protected set; "; break;
					case AccessModifier.Private: header += "private set; "; break;
				}
			}
			header += "}";

			return header;
		}

		public static string ToHeader(this DotNetIndexer indexer)
		{
			return indexer.TypeName.ToDisplayStringLink() + " this[" + String.Join(",", indexer.Parameters.Select(p => p.TypeName.ToDisplayStringLink()).ToArray()) + "]";
		}

		public static string ToHeader(this DotNetParameter parameter)
		{
			return parameter.TypeName.ToDisplayStringLink() + " " + parameter.Name;
		}

		public static string ToDisplayString(this DotNetParameter parameter)
		{
			if(parameter.TypeName == null)
				return parameter.Name;
			if(String.IsNullOrEmpty(parameter.Name))
				return parameter.TypeName.ToDisplayStringLink();
			return parameter.TypeName.ToDisplayStringLink() + " " + parameter.Name;
		}

		public static string ToDisplayString(this DotNetQualifiedName name, string _namespace = null)
		{
			if(name == null)
				return "";

			if(_namespace == null)
				_namespace = "";
			_namespace += ".";

			string displayString = name.FullName;
			if(displayString.StartsWith(_namespace))
			{
				displayString = displayString.Substring(_namespace.Length);
			}

			displayString = displayString.Replace("<", "&lt;").Replace(">", "&gt;"); //markdown understands html tags

			return displayString;
		}

		public static string ToDisplayStringLink(this DotNetQualifiedName name, string _namespace = null)
		{
			string displayString = name.ToDisplayString();
			if(InternalFullNames.Contains(displayString))
			{
				displayString = String.Format("[{0}]({1})", name.ToDisplayString(_namespace), displayString + Ext.MD);
			}
			else if(name.IsInKnownMicrosoftNamespace())
			{
				TurnQualifiedNameConverterOff();
				//todo: convert generic type parameters from <T> style to `1 style
				string microsoftDocumentation = @"https://docs.microsoft.com/en-us/dotnet/api/";
				displayString = String.Format("[{0}]({1}{2})", displayString, microsoftDocumentation, name.FullName.ToMicrosoftLinkFormat());
				TurnQualifiedNameConverterOn();
			}
			return displayString;
		}

		private static string ToMicrosoftLinkFormat(this string name)
		{
			name = name.ToLower();

			List<string> segments = name.SplitIgnoreNested('.').ToList();
			for(int i = 0; i < segments.Count; i++)
			{
				string segment = segments[i];
				if(segment.IndexOf('<') == -1) continue;

				string genericTypeParameters = segment.Substring(segment.IndexOf('<'));
				string[] parameters = genericTypeParameters.RemoveOuterBraces().SplitIgnoreNested(',');

				segment = segment.Replace(genericTypeParameters, "-" + parameters.Length);
				segments[i] = segment;
			}
			return String.Join(".", segments.ToArray());
		}

		public static void TurnQualifiedNameConverterOn()
		{
			DotNetSettings.QualifiedNameConverter = DotNetSettings.DefaultQualifiedNameConverter;
			DotNetSettings.AdditionalQualifiedNameConverter = DotNetExtensions.QualifiedNameConverter;
		}

		public static void TurnQualifiedNameConverterOff()
		{
			DotNetSettings.QualifiedNameConverter = null;
			DotNetSettings.AdditionalQualifiedNameConverter = null;
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
			AddFloatingComments(typeSection, type as DotNetMember);
			AddTopLevelExamples(typeSection, type as DotNetMember);
			AddTopLevelPermissions(typeSection, type as DotNetMember);
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
						fieldSection.AddSection(ToMarkdownSection(field, type));
					}
				}
				if(type.NormalFields.Count > 0)
				{
					foreach(DotNetField field in type.NormalFields.OrderBy(m => m.Name.LocalName))
					{
						fieldSection.AddSection(ToMarkdownSection(field, type));
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
						propertySection.AddSection(ToMarkdownSection(i as DotNetField, type));
					}
				}
				if(type.NormalProperties.Count > 0)
				{
					foreach(DotNetProperty p in type.NormalProperties.OrderBy(m => m.Name.LocalName))
					{
						propertySection.AddSection(ToMarkdownSection(p as DotNetField, type));
					}
				}
			}
			if(type.Events.Count > 0)
			{
				MarkdownSection eventSection = new MarkdownSection("Events");
				typeSection.Add(eventSection);
				foreach(DotNetEvent e in type.Events.OrderBy(m => m.Name.LocalName))
				{
					eventSection.AddSection(ToMarkdownSection(e as DotNetField, type));
				}
			}
			if(type.Delegates.Count > 0)
			{
				MarkdownSection delegateSection = new MarkdownSection("Delegates");
				typeSection.Add(delegateSection);
				foreach(DotNetDelegate _delegate in type.Delegates.OrderBy(m => m.Name.LocalName))
				{
					delegateSection.AddInLine(new MarkdownInlineLink(_delegate.Name.LocalName, _delegate.Name.FullName + Ext.MD));
					delegateSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(_delegate.SummaryComments));
					delegateSection.Add(new MarkdownLine());
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

		public static MarkdownSection ToMarkdownSection(this DotNetField field, DotNetType parent)
		{
			MarkdownSection memberSection = new MarkdownSection(field.ToHeader(parent));

			AddSummary(memberSection, field as DotNetMember);
			AddValue(memberSection, field as DotNetMember);
			AddRemarks(memberSection, field as DotNetMember);
			AddFloatingComments(memberSection, field as DotNetMember);
			AddExamples(memberSection, field as DotNetMember);
			AddPermissions(memberSection, field as DotNetMember);

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
			if(method.Category == MethodCategory.Delegate)
				header = "delegate " + header;

			string fullHeader = header;
			if(method is DotNetDelegate)
			{
				header = method.Name.LocalName;
			}

			MarkdownSection memberSection = new MarkdownSection(header);

			if(method is DotNetDelegate)
			{
				memberSection.AddInParagraph(MarkdownText.Bold(fullHeader));
			}

			AddSummary(memberSection, method as DotNetMember);
			AddRemarks(memberSection, method as DotNetMember);
			AddFloatingComments(memberSection, method as DotNetMember);
			AddReturns(memberSection, method as DotNetMember);
			if(method.Category == MethodCategory.Delegate)
			{
				AddTopLevelExamples(memberSection, method as DotNetMember);
				AddTopLevelPermissions(memberSection, method as DotNetMember);
			}
			else
			{
				AddExamples(memberSection, method as DotNetMember);
				AddPermissions(memberSection, method as DotNetMember);
			}
			AddExceptions(memberSection, method as DotNetMember);
			if(method.Category == MethodCategory.Delegate)
			{
				AddTopLevelParameters(memberSection, method);
			}
			else
			{
				AddParameters(memberSection, method);
			}

			return memberSection;
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
				MarkdownLine inheritanceLine = new MarkdownLine(type.BaseType.Name.ToDisplayStringLink(type.Name.FullNamespace));
				DotNetBaseType baseType = type.BaseType.BaseType;
				while(baseType != null)
				{
					inheritanceLine.Prepend(baseType.Name.ToDisplayStringLink(type.Name.FullNamespace) + " → ");
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
				interfaceLine.Add(String.Join(", ", type.ImplementedInterfaces.Select(i => i.Name.ToDisplayStringLink(type.Name.FullNamespace)).ToArray()));
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
		}

		private static void AddValue(MarkdownSection section, DotNetMember member)
		{
			if(member.ValueComments.Count == 0)
				return;

			if(!section.IsEmpty)
			{
				section.Add(new MarkdownLine("<hr/>"));
				section.Add(new MarkdownLine(MarkdownText.Bold("Value:")));
			}

			section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(member.ValueComments));
		}

		private static void AddRemarks(MarkdownSection section, DotNetMember member)
		{
			if(member.RemarksComments.Count == 0)
				return;

			if(!section.IsEmpty)
				section.Add(new MarkdownLine("<hr/>"));

			section.Add(new MarkdownLine(MarkdownText.Bold("Remarks:")));
			section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(member.RemarksComments));
		}

		private static void AddReturns(MarkdownSection section, DotNetMember member)
		{
			if(member.ReturnsComments.IsEmpty)
				return;

			section.Add(new MarkdownLine(MarkdownText.Bold("Returns:")));
			section.Add(ConvertDotNet.DotNetCommentsToMarkdown(member.ReturnsComments));
		}

		private static void AddTopLevelExamples(MarkdownSection section, DotNetMember member)
		{
			if(member.ExampleComments.Count == 0)
				return;

			MarkdownSection examplesSection = new MarkdownSection("Examples");
			section.Add(examplesSection);

			AlphabetCounter counter = new AlphabetCounter();
			foreach(DotNetComment comment in member.ExampleComments)
			{
				string exampleHeader = "Example " + counter.Value + ":";
				MarkdownSection exampleSection = examplesSection.AddSection(exampleHeader);
				exampleSection.Add(ConvertDotNet.DotNetCommentsToMarkdown(comment));
				counter++;
			}
		}

		private static void AddExamples(MarkdownSection section, DotNetMember member)
		{
			if(member.ExampleComments.Count == 0)
				return;

			if(!section.IsEmpty)
				section.Add(new MarkdownLine("<hr/>"));

			AlphabetCounter counter = new AlphabetCounter();
			foreach(DotNetComment comment in member.ExampleComments)
			{
				string exampleHeader = "Example " + counter.Value + ":";
				section.AddInLine(MarkdownText.Bold(exampleHeader));
				section.Add(ConvertDotNet.DotNetCommentsToMarkdown(comment));
				counter++;
			}
			section.Add(new MarkdownLine());
		}

		private static void AddTopLevelPermissions(MarkdownSection section, DotNetMember member)
		{
			if(member.PermissionComments.Count == 0)
				return;

			MarkdownSection permissionsSection = new MarkdownSection("Permissions");
			section.Add(permissionsSection);

			foreach(DotNetCommentQualifiedLinkedGroup comment in member.PermissionComments)
			{
				string permissionHeader = comment.QualifiedLink.Name.ToDisplayString(member.Name.FullNamespace);
				if(member.Name == comment.QualifiedLink.Name) //todo: move link to member comparison to its own method, maybe even in DataFiles.DotNet
				{
					permissionHeader = "current member";
				}
				if(comment is DotNetCommentMethodLinkedGroup && member is DotNetMethod)
				{
					if((member as DotNetMethod).MatchesSignature((comment as DotNetCommentMethodLinkedGroup).MethodLink))
					{
						permissionHeader = "current member";
					}
				}
				MarkdownSection permissionSection = permissionsSection.AddSection(permissionHeader);
				permissionSection.Add(ConvertDotNet.DotNetCommentsToMarkdown(comment));
			}
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

		private static void AddFloatingComments(MarkdownSection section, DotNetMember member)
		{
			if(member.FloatingComments.IsEmpty)
				return;

			if(!section.IsEmpty)
				section.Add(new MarkdownLine("<hr/>"));

			section.Add(new MarkdownLine(MarkdownText.Bold("Misc:")));
			section.Add(ConvertDotNet.DotNetCommentsToMarkdown(member.FloatingComments));
		}

		public static void AddTopLevelParameters(MarkdownSection section, DotNetMethod method)
		{
			if(method.ParameterComments.Count == 0)
				return;

			MarkdownSection parametersSection = new MarkdownSection("Parameters");
			section.Add(parametersSection);

			if(EachCommentIsOneTextComment(method.ParameterComments)) //todo: partially duplicated with AddParameters method
			{
				MarkdownList list = new MarkdownList(isNumbered: false);
				section.Add(list);
				foreach(DotNetParameter parameter in method.Parameters) //display in same order as in method signature
				{
					DotNetCommentParameter commentParameter = method.ParameterComments.FirstOrDefault(c => c.ParameterLink.Name == parameter.Name);
					if(commentParameter == null)
						continue;

					MarkdownLine line = new MarkdownLine(MarkdownText.Italic(parameter.ToHeader()), new MarkdownText(": "));
					line.Add(ConvertDotNet.DotNetCommentGroupToMarkdownLine(commentParameter));
					list.Add(line);
				}
			}
			else
			{
				foreach(DotNetParameter parameter in method.Parameters) //display in same order as in method signature
				{
					DotNetCommentParameter commentParameter = method.ParameterComments.FirstOrDefault(c => c.ParameterLink.Name == parameter.Name);
					if(commentParameter == null)
						continue;

					MarkdownSection parameterSection = parametersSection.AddSection(parameter.ToHeader());
					parameterSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(commentParameter));
				}
			}
		}

		public static void AddParameters(MarkdownSection section, DotNetMethod method)
		{
			if(method.ParameterComments.Count == 0)
				return;

			if(!section.IsEmpty)
				section.Add(new MarkdownLine("<hr/>"));

			section.Add(new MarkdownParagraph(MarkdownText.Bold("Parameters:")));
			if(EachCommentIsOneTextComment(method.ParameterComments))
			{
				MarkdownList list = new MarkdownList(isNumbered: false);
				section.Add(list);
				foreach(DotNetParameter parameter in method.Parameters) //display in same order as in method signature
				{
					DotNetCommentParameter commentParameter = method.ParameterComments.FirstOrDefault(c => c.ParameterLink.Name == parameter.Name);
					if(commentParameter == null)
						continue;

					MarkdownLine line = new MarkdownLine(MarkdownText.Italic(parameter.ToHeader()), new MarkdownText(": "));
					line.Add(ConvertDotNet.DotNetCommentGroupToMarkdownLine(commentParameter));
					list.Add(line);
				}
			}
			else
			{
				foreach(DotNetParameter parameter in method.Parameters) //display in same order as in method signature; todo: repeated generator structure
				{
					DotNetCommentParameter commentParameter = method.ParameterComments.FirstOrDefault(c => c.ParameterLink.Name == parameter.Name);
					if(commentParameter == null)
						continue;

					section.Add(MarkdownText.Bold(parameter.ToHeader()));
					section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(commentParameter));
				}
			}
			section.Add(new MarkdownLine());
		}

		public static void AddTopLevelExceptions(MarkdownSection section, DotNetMember member)
		{
			if(member.ExceptionComments.Count == 0)
				return;

			MarkdownSection exceptionsSection = new MarkdownSection("Exceptions");
			section.Add(exceptionsSection);

			if(EachCommentIsOneTextComment(member.ExceptionComments)) //todo: partially duplicated with AddExceptions method
			{
				MarkdownList list = new MarkdownList(isNumbered: false);
				section.Add(list);
				foreach(DotNetCommentQualifiedLinkedGroup comment in member.ExceptionComments)
				{
					MarkdownLine line = new MarkdownLine(MarkdownText.Italic(comment.QualifiedLink.Name.FullName), new MarkdownText(": "));
					line.Add(ConvertDotNet.DotNetCommentGroupToMarkdownLine(comment));
					list.Add(line);
				}
			}
			else
			{
				foreach(DotNetCommentQualifiedLinkedGroup comment in member.ExceptionComments)
				{
					MarkdownSection exceptionSection = exceptionsSection.AddSection(comment.QualifiedLink.Name.FullName);
					exceptionSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(comment));
				}
			}
		}

		private static void AddExceptions(MarkdownSection section, DotNetMember member)
		{
			if(member.ExceptionComments.Count == 0)
				return;

			if(!section.IsEmpty)
				section.Add(new MarkdownLine("<hr/>"));

			section.Add(new MarkdownLine(MarkdownText.Bold("Exceptions:")));

			if(EachCommentIsOneTextComment(member.ExceptionComments))
			{
				MarkdownList list = new MarkdownList(isNumbered: false);
				section.Add(list);
				foreach(DotNetCommentQualifiedLinkedGroup comment in member.ExceptionComments)
				{
					MarkdownLine line = new MarkdownLine(MarkdownText.Italic(comment.QualifiedLink.Name.FullName), new MarkdownText(": "));
					line.Add(ConvertDotNet.DotNetCommentGroupToMarkdownLine(comment));
					list.Add(line);
				}
			}
			else
			{
				foreach(DotNetCommentQualifiedLinkedGroup comment in member.ExceptionComments)
				{
					section.Add(MarkdownText.Italic(comment.QualifiedLink.Name.FullName + ":"), new MarkdownText(" "));
					section.Add(ConvertDotNet.DotNetCommentsToMarkdown(comment));
				}
			}
			section.Add(new MarkdownLine());
		}

		/// <summary>
		/// Returns true if each comment boils down to just one text comment, or no comments at all.
		/// </summary>
		private static bool EachCommentIsOneTextComment(List<DotNetCommentParameter> comments)
		{
			foreach(DotNetCommentParameter comment in comments)
			{
				if(!CommentIsOneTextComment(comment as DotNetCommentGroup))
					return false;
			}
			return true;
		}

		/// <summary>
		/// Returns true if each comment boils down to just one text comment, or no comments at all.
		/// </summary>
		private static bool EachCommentIsOneTextComment(List<DotNetCommentQualifiedLinkedGroup> comments)
		{
			foreach(DotNetCommentQualifiedLinkedGroup comment in comments)
			{
				if(!CommentIsOneTextComment(comment as DotNetCommentGroup))
					return false;
			}
			return true;
		}

		/// <summary>
		/// Returns true if this whole comment boils down to just one text comment, or no comments at all.
		/// </summary>
		private static bool CommentIsOneTextComment(DotNetCommentGroup group)
		{
			if(group.IsEmpty)
				return true;
			if(group.Count > 1)
				return false;
			return (group[0] is DotNetCommentText);
		}
	}
}
