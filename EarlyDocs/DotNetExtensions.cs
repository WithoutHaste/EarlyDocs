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
		/// <summary>List of all full names of known types/delegates in the assembly being documented.</summary>
		public static List<string> InternalFullNames = new List<string>();

		/// <summary>List of all internal types that implement each internal interface.</summary>
		public static Dictionary<DotNetQualifiedName, List<DotNetType>> InterfaceImplementedByTypes = new Dictionary<DotNetQualifiedName, List<DotNetType>>();

		public static List<DotNetQualifiedName> KnownMicrosoftNamespaces = new List<DotNetQualifiedName>() {
			DotNetQualifiedName.FromVisualStudioXml("System"),
		};

		public static Dictionary<string, string> UnaryOperators = new Dictionary<string, string>() {
				{ "op_True", "(true)" },
				{ "op_False", "(false)" },
				{ "op_OnesComplement", "~" },
				{ "op_LogicalNot", "!" },
				{ "op_Decrement", "--" },
				{ "op_Increment", "++" },
				{ "op_Implicit", "implicit" },
				{ "op_Explicit", "explicit" },
			};
		public static Dictionary<string, string> BinaryOperators = new Dictionary<string, string>() {
				{ "op_Addition", "+" },
				{ "op_Subtraction", "-" },
				{ "op_Multiply", "*" },
				{ "op_Division", "/" },
				{ "op_Modulus", "%" },
				{ "op_BitwiseAnd", "&" },
				{ "op_BitwiseOr", "|" },
				{ "op_ExclusiveOr", "^" },
				{ "op_Equality", "==" },
				{ "op_Inequality", "!=" },
				{ "op_GreaterThan", ">" },
				{ "op_LessThan", "<" },
				{ "op_GreaterThanOrEqual", ">=" },
				{ "op_LessThanOrEqual", "<=" },
				{ "op_RightShift", ">>" },
				{ "op_LeftShift", "<<" },
			};

		public static bool IsInKnownMicrosoftNamespace(this DotNetQualifiedName name)
		{
			if(name == null)
				return false;
			if(KnownMicrosoftNamespaces.Contains(name))
				return true;
			if(name.FullNamespace != null)
			{
				if(name.FullNamespace.IsInKnownMicrosoftNamespace())
					return true;
			}
			return false;
		}

		//---------------------------------------------------------------------------------

		public static string ToHeader(this DotNetField field, DotNetType parent)
		{
			if(field is DotNetProperty)
				return ToHeader(field as DotNetProperty, parent);
			if(parent.Category == TypeCategory.Enum)
				return field.ConstantValue.ToString() + ": " + field.Name.LocalName;

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

			if(property.Name.ExplicitInterface != null)
				header = property.TypeName.ToDisplayStringLink(parent.Name.FullName) + " " + property.Name.ExplicitInterface.ToDisplayStringLink(parent.Name.FullName) + "." + property.Name.LocalName;

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
			string header = indexer.TypeName.ToDisplayStringLink() + " this[" + String.Join(",", indexer.Parameters.Select(p => p.TypeName.ToDisplayStringLink() + " " + p.Name).ToArray()) + "]";

			//todo: duplicated from Property.ToHeader()
			header += " { ";
			if(indexer.HasGetterMethod)
			{
				switch(indexer.GetterMethod.AccessModifier)
				{
					case AccessModifier.Public: header += "get; "; break;
					case AccessModifier.Protected: header += "protected get; "; break;
					case AccessModifier.Internal: header += "internal get; "; break;
					case AccessModifier.InternalProtected: header += "internal protected get; "; break;
					case AccessModifier.Private: header += "private get; "; break;
				}
			}
			if(indexer.HasSetterMethod)
			{
				switch(indexer.SetterMethod.AccessModifier)
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

		public static string ToHeader(this DotNetParameter parameter, string _namespace = null)
		{
			return parameter.TypeName.ToDisplayStringLink(_namespace) + " " + parameter.Name;
		}

		public static string ToHeader(this DotNetCommentParameter commentParameter)
		{
			return commentParameter.ParameterLink.Name;
		}

		public static string ToHeader(this DotNetMethod method)
		{
			if(method is DotNetMethodOperator)
			{
				return ToHeader(method as DotNetMethodOperator);
			}

			string header = method.MethodName.ReturnTypeName.ToDisplayStringLink(method.Name.FullNamespace) + " " + method.Name.LocalName;
			if(method is DotNetMethodConstructor)
			{
				header = method.Name.FullNamespace.LocalName;
			}
			else if(method.Name.ExplicitInterface != null)
			{
				header = method.MethodName.ReturnTypeName.ToDisplayString(method.Name.FullNamespace) + " " + method.Name.ExplicitInterface.ToDisplayStringLink() + "." + method.Name.LocalName;
			}

			if(method.MethodName.Parameters == null || method.MethodName.Parameters.Count == 0)
				header += "()";
			else
				header += String.Format("({0})", String.Join(", ", method.MethodName.Parameters.Select(p => p.ToDisplayString(method.Name.FullNamespace)).ToArray()));

			if(method.Category == MethodCategory.Virtual)
				header = "virtual " + header;
			if(method.Category == MethodCategory.Static)
				header = "static " + header;
			if(method.Category == MethodCategory.Abstract)
				header = "abstract " + header;
			if(method.Category == MethodCategory.Delegate)
				header = "delegate " + header;

			return header;
		}

		public static string ToHeader(this DotNetMethodOperator method)
		{
			string key = method.Name.LocalName;
			string _namespace = method.Name.FullNamespace;

			string returnType = method.MethodName.ReturnTypeName.ToDisplayStringLink(_namespace);
			if(BinaryOperators.ContainsKey(key) && method.MethodName.Parameters.Count >= 2)
			{
				string parameterA = method.MethodName.Parameters[0].ToHeader(_namespace);
				string parameterB = method.MethodName.Parameters[1].ToHeader(_namespace);
				return String.Format("{0} = {1} {2} {3}", returnType, parameterA, BinaryOperators[key], parameterB);
			}
			else if(UnaryOperators.ContainsKey(key) && method.MethodName.Parameters.Count >= 1)
			{
				string parameterA = method.MethodName.Parameters[0].ToHeader(_namespace);
				if(key == "op_Increment" || key == "op_Decrement")
				{
					return String.Format("{0} = ({1}){2}", returnType, parameterA, UnaryOperators[key]);
				}
				else if(key == "op_Implicit" || key == "op_Explicit")
				{
					return String.Format("{0} {1}({2})", UnaryOperators[key], returnType, parameterA);
				}
				return String.Format("{0} = {1}({2})", returnType, UnaryOperators[key], parameterA);
			}
			return "unknown operator";
		}

		public static string ToDisplayString(this DotNetParameter parameter, string _namespace = null)
		{
			string prefix = "";
			if(parameter.Category == ParameterCategory.Out)
				prefix = "out ";
			if(parameter.Category == ParameterCategory.Ref)
				prefix = "ref ";

			string suffix = "";
			if(parameter.Category == ParameterCategory.Optional)
			{
				if(parameter.DefaultValue == null)
					suffix = " = null";
				else
					suffix = " = " + parameter.DefaultValue.ToString();
			}

			if(parameter.TypeName == null)
				return prefix + parameter.Name + suffix;
			if(String.IsNullOrEmpty(parameter.Name))
				return prefix + parameter.TypeName.ToDisplayStringLink(_namespace) + suffix;
			return prefix + parameter.TypeName.ToDisplayStringLink(_namespace) + " " + parameter.Name + suffix;
		}

		public static string ToDisplayString(this DotNetCommentMethodLink methodLink, string _namespace = null)
		{
			return methodLink.Name.ToDisplayString(_namespace);
		}

		//todo: instead of passing "parent namespace" as a string, pass it as DotNetQualifiedName and use a new child.Localize(parent) method

		public static string ToDisplayString(this DotNetQualifiedName name, string _namespace = null)
		{
			if(name is DotNetQualifiedMethodName)
				return (name as DotNetQualifiedMethodName).ToDisplayString(_namespace);

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

		public static string ToDisplayString(this DotNetQualifiedMethodName name, string _namespace = null)
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

			displayString += name.ParametersWithoutNames;

			displayString = displayString.Replace("#cctor", name.FullNamespace.LocalName);
			displayString = displayString.Replace("#ctor", name.FullNamespace.LocalName);

			displayString = displayString.Replace("<", "&lt;").Replace(">", "&gt;"); //markdown understands html tags

			return displayString;
		}

		public static string ToDisplayStringLink(this DotNetQualifiedName name, string _namespace = null)
		{
			if(name == null)
				return "";
			if(name == _namespace)
				return name.LocalName; //no need to link to same page, no need to show full path to current page

			string displayString = name.ToDisplayString(_namespace);
			string linkString = name.ToStringLink();

			if(!linkString.EndsWith(Ext.MD) && !linkString.Contains("http")) //todo: refactor: duplicates logic from ConvertDotNet.DotNetCommentsToMarkdown
			{
				return displayString;
			}
			return String.Format("[{0}]({1})", displayString, linkString);
		}

		public static string ToStringLink(this DotNetQualifiedName name)
		{
			string linkString = name.FullName;
			string parentLinkString = name.FullNamespace?.FullName;
			if(name.IsInKnownMicrosoftNamespace())
			{
				TurnQualifiedNameConverterOff();
				string microsoftDocumentation = @"https://docs.microsoft.com/en-us/dotnet/api/";
				linkString = String.Format("{0}{1}", microsoftDocumentation, name.FullName.ToMicrosoftLinkFormat());
				TurnQualifiedNameConverterOn();
			}
			else if(InternalFullNames.Contains(linkString))
			{
				linkString = ConvertXML.FormatFilename(linkString) + Ext.MD;
			}
			else if(InternalFullNames.Contains(parentLinkString))
			{
				linkString = ConvertXML.FormatFilename(parentLinkString) + Ext.MD;
			}
			return linkString;
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

		//---------------------------------------------------------------------------------

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

		//---------------------------------------------------------------------------------

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
			string header = String.Format("[{0}]({1}).{2}", type.Name.FullNamespace.FullName, ConvertXML.TableOfContentsFilename(type.Name.FullNamespace), type.Name.LocalName);
			if(InternalFullNames.Contains(type.Name.FullNamespace.FullName))
				header = String.Format("[{0}]({1}).{2}", type.Name.FullNamespace.FullName, type.Name.FullNamespace.FullName + Ext.MD, type.Name.LocalName);

			MarkdownSection typeSection = new MarkdownSection(header);

			AddPreSummary(typeSection, type);
			AddSummary(typeSection, type as DotNetMember);
			AddRemarks(typeSection, type as DotNetMember);
			AddFloatingComments(typeSection, type as DotNetMember);
			AddTopLevelTypeParameters(typeSection, type as DotNetMember);
			AddTopLevelExamples(typeSection, type as DotNetMember);
			AddTopLevelPermissions(typeSection, type as DotNetMember);
			if(type.Category == TypeCategory.Interface && InterfaceImplementedByTypes.ContainsKey(type.Name))
			{
				MarkdownSection implementedBySection = new MarkdownSection("Implemented By");
				typeSection.Add(implementedBySection);
				foreach(DotNetType implementedBy in InterfaceImplementedByTypes[type.Name].OrderBy(t => t.Name))
				{
					implementedBySection.AddInLine(implementedBy.Name.ToDisplayStringLink());
					implementedBySection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(implementedBy.SummaryComments));
				}
			}
			if(type.NestedEnums.Count > 0)
			{
				MarkdownSection enumSection = new MarkdownSection("Enums");
				typeSection.Add(enumSection);
				foreach(DotNetType e in type.NestedEnums.OrderBy(m => m.Name.LocalName))
				{
					string enumHeader = e.Name.ToDisplayStringLink(type.Name);
					enumSection.Add(new MarkdownLine(MarkdownText.Bold(enumHeader)));
					enumSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(e.SummaryComments));
					enumSection.Add(ConvertDotNet.EnumToMinimalMDList(e));
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
				}
			}

			if(type.ConstructorMethods.Count > 0)
				typeSection.Add(MethodsToMarkdown("Constructors", type.ConstructorMethods.Cast<DotNetMethod>().ToList()));
			if(type.DestructorMethod != null)
				typeSection.Add(MethodsToMarkdown("Destructor", new List<DotNetMethod>() { type.DestructorMethod as DotNetMethod }));
			if(type.NormalMethods.Count > 0)
				typeSection.Add(MethodsToMarkdown("Methods", type.NormalMethods));
			if(type.StaticMethods.Count > 0)
				typeSection.Add(MethodsToMarkdown("Static Methods", type.StaticMethods));
			if(type.OperatorMethods.Count > 0)
				typeSection.Add(MethodOperatorsToMarkdown("Operators", type.OperatorMethods));

			if(type.NestedTypes.Count > 0)
			{
				MarkdownSection nestedTypeSection = new MarkdownSection("Nested Types");
				typeSection.Add(nestedTypeSection);
				foreach(DotNetType nestedType in type.NestedTypes.OrderBy(m => m.Name))
				{
					nestedTypeSection.AddInLine(new MarkdownInlineLink(nestedType.Name.LocalName, nestedType.Name.FullName + Ext.MD));
					nestedTypeSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(nestedType.SummaryComments));
				}
			}

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
			string header = method.ToHeader();
			string fullHeader = header;
			if(method is DotNetDelegate)
			{
				if(InternalFullNames.Contains(method.Name.FullNamespace.FullName))
				{
					header = String.Format("[{0}]({1}).{2}", method.Name.FullNamespace.FullName, method.Name.FullNamespace + Ext.MD, method.Name.LocalName);
				}
				else
				{
					header = String.Format("[{0}]({1}).{2}", method.Name.FullNamespace.FullName, ConvertXML.TableOfContentsFilename(method.Name.FullNamespace), method.Name.LocalName);
				}
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
				AddTopLevelExceptions(memberSection, method as DotNetMember);
				AddTopLevelTypeParameters(memberSection, method as DotNetMember);
				AddTopLevelParameters(memberSection, method);
			}
			else
			{
				AddExamples(memberSection, method as DotNetMember);
				AddPermissions(memberSection, method as DotNetMember);
				AddExceptions(memberSection, method as DotNetMember);
				AddTypeParameters(memberSection, method as DotNetMember);
				AddParameters(memberSection, method);
			}

			return memberSection;
		}

		public static MarkdownSection ToMarkdownEnumSection(this DotNetType type)
		{
			if(type.Category != TypeCategory.Enum)
				return new MarkdownSection("");

			MarkdownSection enumSection = new MarkdownSection(type.Name.LocalName);

			AddSummary(enumSection, type as DotNetMember);
			AddRemarks(enumSection, type as DotNetMember);
			AddFloatingComments(enumSection, type as DotNetMember);
			AddTopLevelExamples(enumSection, type as DotNetMember);
			AddTopLevelPermissions(enumSection, type as DotNetMember);

			if(type.Fields.Count > 0)
			{
				MarkdownSection fieldSection = new MarkdownSection("Constants");
				enumSection.Add(fieldSection);
				foreach(DotNetField field in type.Fields.OrderBy(f => f.ConstantValue))
				{
					fieldSection.AddSection(ToMarkdownSection(field, type));
				}
			}

			return enumSection;
		}

		private static void AddPreSummary(MarkdownSection parent, DotNetType type)
		{
			MarkdownParagraph paragraph = new MarkdownParagraph();

			switch(type.Category)
			{
				case TypeCategory.Static: paragraph.Add(new MarkdownLine(MarkdownText.Bold("Static"))); break;
				case TypeCategory.Interface: paragraph.Add(new MarkdownLine(MarkdownText.Bold("Interface"))); break;
				case TypeCategory.Abstract: paragraph.Add(new MarkdownLine(MarkdownText.Bold("Abstract"))); break;
				case TypeCategory.Enum: paragraph.Add(new MarkdownLine(MarkdownText.Bold("Enumeration"))); break;
				case TypeCategory.Struct: paragraph.Add(new MarkdownLine(MarkdownText.Bold("Struct"))); break;
			}
			if(type.IsSealed && type.Category != TypeCategory.Static && type.Category != TypeCategory.Struct)
			{
				paragraph.Add(new MarkdownLine(MarkdownText.Bold("Sealed")));
			}

			if(type.BaseType != null && type.Category != TypeCategory.Struct)
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
				paragraph.Add(inheritanceLine);
			}

			if(type.ImplementedInterfaces.Count > 0)
			{
				MarkdownLine interfaceLine = new MarkdownLine(MarkdownText.Bold("Implements:"), new MarkdownText(" "));
				interfaceLine.Add(String.Join(", ", type.ImplementedInterfaces.Select(i => i.Name.ToDisplayStringLink(type.Name.FullNamespace)).ToArray()));
				paragraph.Add(interfaceLine);
			}

			if(!paragraph.IsEmpty)
				parent.Add(paragraph);
		}

		private static MarkdownSection MethodsToMarkdown(string header, List<DotNetMethod> methods)
		{
			MarkdownSection methodSection = new MarkdownSection(header);
			foreach(DotNetMethod method in methods.OrderBy(m => m.Name.LocalName))
			{
				methodSection.AddSection(ToMarkdownSection(method));
			}
			return methodSection;
		}

		private static MarkdownSection MethodOperatorsToMarkdown(string header, List<DotNetMethodOperator> methods)
		{
			MarkdownSection methodSection = new MarkdownSection(header);
			foreach(DotNetMethod method in methods.OrderBy(m => m))
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
				section.Add(new MarkdownLine(MarkdownText.Bold("Value:")));
			}

			section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(member.ValueComments));
		}

		private static void AddRemarks(MarkdownSection section, DotNetMember member)
		{
			if(member.RemarksComments.Count == 0)
				return;

			section.Add(new MarkdownLine(MarkdownText.Bold("Remarks:")));
			section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(member.RemarksComments));
		}

		private static void AddReturns(MarkdownSection section, DotNetMember member)
		{
			if(member.ReturnsComments.IsEmpty)
				return;

			section.Add(new MarkdownLine(MarkdownText.Bold("Returns:")));
			section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(member.ReturnsComments));
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
				exampleSection.Add(ConvertDotNet.DotNetCommentsToParagraph(comment));
				counter++;
			}
		}

		private static void AddExamples(MarkdownSection section, DotNetMember member)
		{
			if(member.ExampleComments.Count == 0)
				return;

			AlphabetCounter counter = new AlphabetCounter();
			foreach(DotNetComment comment in member.ExampleComments)
			{
				string exampleHeader = "Example " + counter.Value + ":";
				section.AddInLine(MarkdownText.Bold(exampleHeader));
				section.Add(ConvertDotNet.DotNetCommentsToParagraph(comment));
				counter++;
			}
		}

		private static void AddTopLevelPermissions(MarkdownSection section, DotNetMember member)
		{
			//todo: so much duplicated logic to refactor

			if(member.PermissionComments.Count == 0)
				return;

			MarkdownSection permissionsSection = new MarkdownSection("Permissions");
			section.Add(permissionsSection);

			foreach(DotNetCommentQualifiedLinkedGroup comment in member.PermissionComments)
			{
				string permissionHeader = comment.QualifiedLink.Name.ToDisplayString(member.Name);
				if(member.Matches(comment))
				{
					permissionHeader = "current member";
					if(member is DotNetType || member is DotNetDelegate)
					{
						permissionHeader = "current type";
					}
				}
				if(comment is DotNetCommentMethodLinkedGroup)
				{
					permissionHeader = (comment as DotNetCommentMethodLinkedGroup).MethodLink.ToDisplayString(member.Name);
					if(member is DotNetMethod && (member as DotNetMethod).MatchesSignature((comment as DotNetCommentMethodLinkedGroup).MethodLink)) //todo: move comparison logic to library
					{
						permissionHeader = "current member";
						if(member is DotNetType || member is DotNetDelegate)
						{
							permissionHeader = "current type";
						}
					}
				}
				MarkdownSection permissionSection = permissionsSection.AddSection(permissionHeader);
				permissionSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(comment));
			}
		}

		private static void AddPermissions(MarkdownSection section, DotNetMember member)
		{
			foreach(DotNetCommentQualifiedLinkedGroup comment in member.PermissionComments)
			{
				string permissionHeader = "Permission: " + comment.QualifiedLink.Name.ToDisplayString(member.Name.FullNamespace);
				if(member.Matches(comment))
				{
					permissionHeader = "Permission: current member";
				}
				if(comment is DotNetCommentMethodLinkedGroup)
				{
					permissionHeader = "Permission: " + (comment as DotNetCommentMethodLinkedGroup).MethodLink.ToDisplayString(member.Name.FullNamespace);
					if(member is DotNetMethod && (member as DotNetMethod).MatchesSignature((comment as DotNetCommentMethodLinkedGroup).MethodLink))
					{
						permissionHeader = "Permission: current member";
					}
					else if(member is DotNetIndexer && (member as DotNetIndexer).Matches((comment as DotNetCommentMethodLinkedGroup).MethodLink))
					{
						permissionHeader = "Permission: current member";
					}
				}
				section.Add(new MarkdownLine(MarkdownText.Bold(permissionHeader)));
				section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(comment));
			}
		}

		private static void AddFloatingComments(MarkdownSection section, DotNetMember member)
		{
			if(member.FloatingComments.IsEmpty)
				return;

			section.Add(new MarkdownLine(MarkdownText.Bold("Misc:")));
			section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(member.FloatingComments, member));
		}

		public static void AddTopLevelTypeParameters(MarkdownSection section, DotNetMember member)
		{
			if(member.TypeParameterComments.Count == 0)
				return;

			MarkdownSection parametersSection = new MarkdownSection("Generic Type Parameters");
			section.Add(parametersSection);

			if(EachCommentIsOneTextComment(member.ParameterComments)) //todo: partially duplicated with AddParameters method
			{
				MarkdownList list = new MarkdownList(isNumbered: false);
				section.Add(list);
				foreach(DotNetCommentParameter commentParameter in member.TypeParameterComments.OrderBy(p => p.ParameterLink.Name))
				{
					MarkdownLine line = new MarkdownLine(MarkdownText.Bold(commentParameter.ToHeader()), new MarkdownText(": "));
					line.Concat(ConvertDotNet.DotNetCommentGroupToMarkdownLine(commentParameter));
					list.Add(line);
				}
			}
			else
			{
				foreach(DotNetCommentParameter commentParameter in member.TypeParameterComments.OrderBy(p => p.ParameterLink.Name))
				{
					section.Add(MarkdownText.Bold(commentParameter.ToHeader()));
					section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(commentParameter));
				}
			}
		}

		public static void AddTypeParameters(MarkdownSection section, DotNetMember member)
		{
			if(member.TypeParameterComments.Count == 0)
				return;

			if(EachCommentIsOneTextComment(member.ParameterComments))
			{
				section.Add(new MarkdownParagraph(MarkdownText.Bold("Generic Type Parameters:")));
				MarkdownList list = new MarkdownList(isNumbered: false);
				section.Add(list);
				foreach(DotNetCommentParameter commentParameter in member.TypeParameterComments.OrderBy(p => p.ParameterLink.Name))
				{
					MarkdownLine line = new MarkdownLine(MarkdownText.Bold(commentParameter.ToHeader()), new MarkdownText(": "));
					line.Concat(ConvertDotNet.DotNetCommentGroupToMarkdownLine(commentParameter));
					list.Add(line);
				}
			}
			else
			{
				MarkdownSection parametersSection = section.AddSection("Generic Type Parameters");
				foreach(DotNetCommentParameter commentParameter in member.TypeParameterComments.OrderBy(p => p.ParameterLink.Name))
				{
					MarkdownSection parameterSection = parametersSection.AddSection(commentParameter.ToHeader());
					AddGroupComments(parameterSection, commentParameter, member);
				}
			}
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
				foreach(DotNetParameter parameter in method.MethodName.Parameters) //display in same order as in method signature
				{
					DotNetCommentParameter commentParameter = method.ParameterComments.FirstOrDefault(c => c.ParameterLink.Name == parameter.Name);
					if(commentParameter == null)
						continue;

					MarkdownLine line = new MarkdownLine(MarkdownText.Bold(parameter.ToHeader(method.Name.FullNamespace)), new MarkdownText(": "));
					line.Concat(ConvertDotNet.DotNetCommentGroupToMarkdownLine(commentParameter));
					list.Add(line);
				}
			}
			else
			{
				foreach(DotNetParameter parameter in method.MethodName.Parameters) //display in same order as in method signature
				{
					DotNetCommentParameter commentParameter = method.ParameterComments.FirstOrDefault(c => c.ParameterLink.Name == parameter.Name);
					if(commentParameter == null)
						continue;

					MarkdownSection parameterSection = parametersSection.AddSection(parameter.ToHeader(method.Name.FullNamespace));
					parameterSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(commentParameter));
				}
			}
		}

		public static void AddParameters(MarkdownSection section, DotNetMethod method)
		{
			if(method.ParameterComments.Count == 0)
				return;

			if(EachCommentIsOneTextComment(method.ParameterComments))
			{
				section.Add(new MarkdownLine(MarkdownText.Bold("Parameters:")));
				MarkdownList list = new MarkdownList(isNumbered: false);
				section.Add(list);
				foreach(DotNetParameter parameter in method.MethodName.Parameters) //display in same order as in method signature
				{
					DotNetCommentParameter commentParameter = method.ParameterComments.FirstOrDefault(c => c.ParameterLink.Name == parameter.Name);
					if(commentParameter == null)
						continue;

					MarkdownLine line = new MarkdownLine(MarkdownText.Bold(parameter.ToHeader(method.Name.FullNamespace)), new MarkdownText(": "));
					line.Concat(ConvertDotNet.DotNetCommentGroupToMarkdownLine(commentParameter));
					list.Add(line);
				}
			}
			else
			{
				MarkdownSection parametersSection = section.AddSection("Parameters");
				foreach(DotNetParameter parameter in method.MethodName.Parameters) //display in same order as in method signature; todo: repeated generator structure
				{
					DotNetCommentParameter commentParameter = method.ParameterComments.FirstOrDefault(c => c.ParameterLink.Name == parameter.Name);
					if(commentParameter == null)
						continue;

					MarkdownSection parameterSection = parametersSection.AddSection(parameter.ToHeader(method.Name.FullNamespace));
					AddGroupComments(parameterSection, commentParameter, method);
				}
			}
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
					MarkdownLine line = new MarkdownLine(MarkdownText.Bold(comment.QualifiedLink.Name.FullName), new MarkdownText(": "));
					line.Concat(ConvertDotNet.DotNetCommentGroupToMarkdownLine(comment));
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

			if(EachCommentIsOneTextComment(member.ExceptionComments))
			{
				section.Add(new MarkdownLine(MarkdownText.Bold("Exceptions:")));
				MarkdownList list = new MarkdownList(isNumbered: false);
				section.Add(list);
				foreach(DotNetCommentQualifiedLinkedGroup comment in member.ExceptionComments)
				{
					MarkdownLine line = new MarkdownLine(MarkdownText.Bold(comment.QualifiedLink.Name.ToDisplayStringLink(member.Name)), new MarkdownText(": "));
					line.Concat(ConvertDotNet.DotNetCommentGroupToMarkdownLine(comment));
					list.Add(line);
				}
			}
			else
			{
				MarkdownSection exceptionsSection = section.AddSection("Exceptions");
				foreach(DotNetCommentQualifiedLinkedGroup comment in member.ExceptionComments)
				{
					MarkdownSection exceptionSection = exceptionsSection.AddSection(comment.QualifiedLink.Name.ToDisplayStringLink(member.Name));
					AddGroupComments(exceptionSection, comment, member);
				}
			}
		}

		private static void AddGroupComments(MarkdownSection section, DotNetCommentLinkedGroup group, DotNetMember parent = null)
		{
			foreach(DotNetComment comment in group.Comments.Where(c => c.Tag != CommentTag.Example))
			{
				section.Add(ConvertDotNet.DotNetCommentsToParagraph(comment, parent));
			}

			//todo: refactor: merge this with method AddExamples somehow
			AlphabetCounter counter = new AlphabetCounter();
			foreach(DotNetComment comment in group.Comments.Where(c => c.Tag == CommentTag.Example))
			{
				string exampleHeader = "Example " + counter.Value + ":";
				section.AddInLine(MarkdownText.Bold(exampleHeader));
				section.Add(ConvertDotNet.DotNetCommentsToParagraph(comment));
				counter++;
			}
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
