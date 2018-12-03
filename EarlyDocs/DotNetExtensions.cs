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
	internal static class DotNetExtensions
	{
		/// <summary>List of all full names of known types/delegates in the assembly being documented.</summary>
		internal static List<string> InternalFullNames = new List<string>();

		/// <summary>List of all internal types that implement each internal interface.</summary>
		internal static Dictionary<DotNetQualifiedName, List<DotNetType>> InterfaceImplementedByTypes = new Dictionary<DotNetQualifiedName, List<DotNetType>>();

		/// <summary>List of all internal types that are direct children of each other type.</summary>
		internal static Dictionary<DotNetQualifiedName, List<DotNetType>> TypeDerivedBy = new Dictionary<DotNetQualifiedName, List<DotNetType>>();

		internal static List<DotNetQualifiedName> KnownMicrosoftNamespaces = new List<DotNetQualifiedName>() {
			DotNetQualifiedName.FromVisualStudioXml("System"),
		};

		internal static Dictionary<string, string> UnaryOperators = new Dictionary<string, string>() {
				{ "op_True", "(true)" },
				{ "op_False", "(false)" },
				{ "op_OnesComplement", "~" },
				{ "op_LogicalNot", "!" },
				{ "op_Decrement", "--" },
				{ "op_Increment", "++" },
				{ "op_Implicit", "implicit" },
				{ "op_Explicit", "explicit" },
			};
		internal static Dictionary<string, string> BinaryOperators = new Dictionary<string, string>() {
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

		internal static List<string> BasicTypeNames = new List<string>() {
			"System.Boolean", "bool",
			"System.Byte", "byte",
			"System.Char", "char",
			"System.Decimal", "decimal",
			"System.Double", "double",
			"System.Int16", "short",
			"System.Int32", "int",
			"System.Int64", "long",
			"System.Object", "object",
			"System.SByte", "sbyte",
			"System.Single", "float",
			"System.String", "string",
			"System.UInt16", "ushort",
			"System.UInt32", "uint",
			"System.UInt64", "ulong",
			"System.Void", "void",
		};

		internal static bool IsInKnownMicrosoftNamespace(this DotNetQualifiedName name)
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

		internal static string ToHeader(this DotNetField field, DotNetType parent)
		{
			if(field is DotNetProperty)
				return ToHeader(field as DotNetProperty, parent);
			if(parent.Category == TypeCategory.Enum)
				return field.ConstantValue.ToString() + ": " + field.Name.LocalName;

			return field.Name.LocalName;
		}

		internal static string ToHeader(this DotNetProperty property, DotNetType parent)
		{
			if(property is DotNetIndexer)
				return ToHeader(property as DotNetIndexer, parent);

			if(property.Name.ExplicitInterface != null)
				return property.Name.ExplicitInterface.ToDisplayStringLink(parent.Name) + "." + property.Name.LocalName;

			return property.Name.LocalName;
		}

		internal static string ToHeader(this DotNetIndexer indexer, DotNetType parent)
		{
			return "this[" + String.Join(",", indexer.Parameters.Select(p => p.TypeName.ToDisplayStringLink(parent.Name) + " " + p.Name).ToArray()) + "]";
		}

		internal static string ToHeader(this DotNetParameter parameter, DotNetQualifiedName _namespace = null)
		{
			return parameter.TypeName.ToDisplayStringLink(_namespace) + " " + parameter.Name;
		}

		internal static string ToHeader(this DotNetCommentParameter commentParameter)
		{
			return commentParameter.ParameterLink.Name;
		}

		internal static string ToHeader(this DotNetMethod method)
		{
			if(method is DotNetMethodOperator)
			{
				return ToHeader(method as DotNetMethodOperator);
			}
			if(method is DotNetDelegate)
			{
				return ToHeader(method as DotNetDelegate);
			}

			string header = method.Name.LocalName;
			if(method is DotNetMethodConstructor)
			{
				header = method.Name.FullNamespace.LocalName;
			}

			if(method.Name.ExplicitInterface != null)
			{
				header = method.Name.ExplicitInterface.ToDisplayStringLink() + "." + header;
			}

			if(method.MethodName.Parameters == null || method.MethodName.Parameters.Count == 0)
				header += "()";
			else
				header += String.Format("({0})", String.Join(", ", method.MethodName.Parameters.Select(p => p.ToDisplayString(method.Name)).ToArray()));

			return header;
		}

		internal static string ToHeader(this DotNetMethodOperator method)
		{
			string key = method.Name.LocalName;
			DotNetQualifiedName _namespace = method.Name.FullNamespace;

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

		internal static string ToHeader(this DotNetDelegate method)
		{
			if(InternalFullNames.Contains(method.Name.FullNamespace.FullName))
				return String.Format("[{0}]({1}).{2}", method.Name.FullNamespace.FullName, method.Name.FullNamespace + Ext.MD, method.Name.LocalName);
			else
				return String.Format("[{0}]({1}).{2}", method.Name.FullNamespace.FullName, ConvertXML.TableOfContentsFilename(method.Name.FullNamespace), method.Name.LocalName);
		}

		internal static string ToDisplayString(this DotNetParameter parameter, DotNetQualifiedName _namespace = null)
		{
			string prefix = "";
			if(parameter.Category == ParameterCategory.Out)
				prefix = "out ";
			if(parameter.Category == ParameterCategory.Ref)
				prefix = "ref ";
			if(parameter.Category == ParameterCategory.Extension)
				prefix = "this ";

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

		internal static string ToDisplayString(this DotNetCommentMethodLink methodLink, DotNetQualifiedName _namespace = null)
		{
			return methodLink.Name.ToDisplayString(_namespace);
		}

		internal static string ToDisplayString(this DotNetQualifiedName name, DotNetQualifiedName _namespace = null)
		{
			if(name is DotNetQualifiedMethodName)
				return (name as DotNetQualifiedMethodName).ToDisplayString(_namespace);

			if(name == null)
				return "";

			string displayString = name.GetLocalized(_namespace).FullName;
			if(name is DotNetQualifiedTypeName)
			{
				displayString = (name as DotNetQualifiedTypeName).GetLocalized(_namespace).FullName;
			}

			displayString = displayString.Replace("<", "&lt;").Replace(">", "&gt;"); //markdown understands html tags

			return displayString;
		}

		internal static string ToDisplayString(this DotNetQualifiedMethodName name, DotNetQualifiedName _namespace = null)
		{
			if(name == null)
				return "";

			string displayString = name.GetLocalized(_namespace).FullName;
			displayString += name.ParametersWithoutNames;

			displayString = displayString.Replace("#cctor", name.FullNamespace.LocalName);
			displayString = displayString.Replace("#ctor", name.FullNamespace.LocalName);

			displayString = displayString.Replace("<", "&lt;").Replace(">", "&gt;"); //markdown understands html tags

			return displayString;
		}

		internal static string ToDisplayStringLink(this DotNetQualifiedName name, DotNetQualifiedName _namespace = null)
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

		internal static string ToStringLink(this DotNetQualifiedName name)
		{
			string linkString = name.FullName;
			string parentLinkString = name.FullNamespace?.FullName;

			if(BasicTypeNames.Contains(linkString))
				return linkString;

			if(name.IsInKnownMicrosoftNamespace())
			{
				TurnQualifiedNameConverterOff();
				string microsoftDocumentation = @"https://docs.microsoft.com/en-us/dotnet/api/";
				linkString = String.Format("{0}{1}", microsoftDocumentation, name.FullName.ToMicrosoftLinkFormat());
				TurnQualifiedNameConverterOn();
				return linkString;
			}

			if(linkString.EndsWith("[]"))
				linkString = linkString.RemoveFromEnd("[]");

			if(InternalFullNames.Contains(linkString))
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
			if(name.EndsWith("[]"))
				return "system.array";

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

		internal static void TurnQualifiedNameConverterOn()
		{
			DotNetSettings.QualifiedNameConverter = DotNetSettings.DefaultQualifiedNameConverter;
			DotNetSettings.AdditionalQualifiedNameConverter = DotNetExtensions.QualifiedNameConverter;
		}

		internal static void TurnQualifiedNameConverterOff()
		{
			DotNetSettings.QualifiedNameConverter = null;
			DotNetSettings.AdditionalQualifiedNameConverter = null;
		}

		internal static string QualifiedNameConverter(string fullName, int depth)
		{
			if(depth > 0)
				return fullName;
			if(String.IsNullOrEmpty(fullName))
				return "";

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

		internal static MarkdownFile ToMarkdownFile(this DotNetType type)
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

		internal static MarkdownSection ToMarkdownSection(this DotNetType type)
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
			if(type.NestedEnums.Count > 0)
			{
				MarkdownSection enumSection = new MarkdownSection("Enums");
				typeSection.Add(enumSection);
				foreach(DotNetType e in type.NestedEnums.OrderBy(m => m.Name.LocalName))
				{
					string enumHeader = e.Name.ToDisplayStringLink(type.Name);
					enumSection.Add(new MarkdownLine(MarkdownText.Bold(enumHeader)));
					enumSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(e.SummaryComments, e));
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
					delegateSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(_delegate.SummaryComments, _delegate));
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
					nestedTypeSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(nestedType.SummaryComments, nestedType));
				}
			}

			if(type.Category == TypeCategory.Interface && InterfaceImplementedByTypes.ContainsKey(type.Name))
			{
				MarkdownSection implementedBySection = new MarkdownSection("Implemented By");
				typeSection.Add(implementedBySection);
				foreach(DotNetType implementedBy in InterfaceImplementedByTypes[type.Name].OrderBy(t => t.Name))
				{
					implementedBySection.AddInLine(implementedBy.Name.ToDisplayStringLink());
					implementedBySection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(implementedBy.SummaryComments, implementedBy));
				}
			}

			if(TypeDerivedBy.ContainsKey(type.Name))
			{
				MarkdownSection derivedBySection = new MarkdownSection("Derived By");
				typeSection.Add(derivedBySection);
				foreach(DotNetType derivedBy in TypeDerivedBy[type.Name].OrderBy(t => t.Name))
				{
					derivedBySection.AddInLine(derivedBy.Name.ToDisplayStringLink());
					derivedBySection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(derivedBy.SummaryComments, derivedBy));
				}
			}

			return typeSection;
		}

		internal static MarkdownFile ToMarkdownFile(this DotNetDelegate _delegate)
		{
			MarkdownFile markdown = new MarkdownFile();

			markdown.AddSection(ToMarkdownSection(_delegate as DotNetMethod));

			return markdown;
		}

		internal static MarkdownSection ToMarkdownSection(this DotNetField field, DotNetType parent)
		{
			MarkdownSection memberSection = new MarkdownSection(field.ToHeader(parent));

			if(parent.Category != TypeCategory.Enum)
			{
				AddPreSummary(memberSection, field);
			}
			AddSummary(memberSection, field as DotNetMember);
			AddValue(memberSection, field as DotNetMember);
			AddRemarks(memberSection, field as DotNetMember);
			AddFloatingComments(memberSection, field as DotNetMember);
			AddExamples(memberSection, field as DotNetMember);
			AddPermissions(memberSection, field as DotNetMember);
			AddExceptions(memberSection, field as DotNetMember);
			if(field is DotNetIndexer)
			{
				AddParameters(memberSection, (field as DotNetIndexer));
			}

			return memberSection;
		}

		internal static MarkdownSection ToMarkdownSection(this DotNetMethod method)
		{
			MarkdownSection memberSection = new MarkdownSection(method.ToHeader());

			AddPreSummary(memberSection, method);
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

		internal static MarkdownSection ToMarkdownEnumSection(this DotNetType type)
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

		private static void AddPreSummary(MarkdownSection section, DotNetType type)
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
				section.Add(paragraph);
		}

		private static void AddPreSummary(MarkdownSection section, DotNetField field)
		{
			if(field is DotNetProperty)
			{
				AddPreSummary(section, field as DotNetProperty);
				return;
			}

			string preSummary = "";

			switch(field.AccessModifier)
			{
				case AccessModifier.Protected: preSummary += "protected "; break;
				case AccessModifier.Internal: preSummary += "internal "; break;
				case AccessModifier.InternalProtected: preSummary += "internal protected "; break;
				case AccessModifier.Private: preSummary += "private "; break;
			}

			switch(field.Category)
			{
				case FieldCategory.Constant: preSummary += "const "; break;
				case FieldCategory.ReadOnly: preSummary += "readonly "; break;
			}

			if(field.IsStatic && field.Category != FieldCategory.Constant)
			{
				preSummary += "static ";
			}

			preSummary += field.TypeName.ToDisplayStringLink(field.Name.FullNamespace);

			if(!String.IsNullOrEmpty(preSummary))
			{
				MarkdownParagraph paragraph = new MarkdownParagraph(MarkdownText.Bold(preSummary));
				section.Add(paragraph);
			}
		}

		private static void AddPreSummary(MarkdownSection section, DotNetProperty property)
		{
			//same for DotNetProperty and DotNetIndexer
			string preSummary = "";

			if(property.Category == FieldCategory.Abstract)
				preSummary += "abstract ";

			preSummary += property.TypeName.ToDisplayStringLink(property.Name.FullNamespace);

			preSummary += " { ";
			if(property.HasGetterMethod)
			{
				switch(property.GetterMethod.AccessModifier)
				{
					case AccessModifier.Public: preSummary += "public get; "; break;
					case AccessModifier.Protected: preSummary += "protected get; "; break;
					case AccessModifier.Internal: preSummary += "internal get; "; break;
					case AccessModifier.InternalProtected: preSummary += "internal protected get; "; break;
					case AccessModifier.Private: preSummary += "private get; "; break;
				}
			}
			if(property.HasSetterMethod)
			{
				switch(property.SetterMethod.AccessModifier)
				{
					case AccessModifier.Public: preSummary += "public set; "; break;
					case AccessModifier.Protected: preSummary += "protected set; "; break;
					case AccessModifier.Internal: preSummary += "internal set; "; break;
					case AccessModifier.InternalProtected: preSummary += "internal protected set; "; break;
					case AccessModifier.Private: preSummary += "private set; "; break;
				}
			}
			preSummary += "}";

			if(!String.IsNullOrEmpty(preSummary))
			{
				MarkdownParagraph paragraph = new MarkdownParagraph(MarkdownText.Bold(preSummary));
				section.Add(paragraph);
			}
		}

		private static void AddPreSummary(MarkdownSection section, DotNetMethod method)
		{
			if(method is DotNetMethodOperator)
			{
				return;
			}

			string preSummary = "";

			switch(method.Category)
			{
				case MethodCategory.Abstract: preSummary += "abstract "; break;
				case MethodCategory.Delegate: preSummary += "delegate "; break;
				case MethodCategory.Extension: preSummary += "static "; break;
				case MethodCategory.Protected: preSummary += "protected "; break;
				case MethodCategory.Static: preSummary += "static "; break;
				case MethodCategory.Virtual: preSummary += "virtual "; break;
			}

			preSummary += method.MethodName.ReturnTypeName.ToDisplayStringLink(method.Name.FullNamespace);

			if(method is DotNetDelegate)
			{
				preSummary += (method as DotNetMethod).ToHeader();
			}

			if(!String.IsNullOrEmpty(preSummary))
			{
				MarkdownParagraph paragraph = new MarkdownParagraph(MarkdownText.Bold(preSummary));
				section.Add(paragraph);
			}
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

			section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(member.SummaryComments, member));
		}

		private static void AddValue(MarkdownSection section, DotNetMember member)
		{
			if(member.ValueComments.Count == 0)
				return;

			if(!section.IsEmpty)
			{
				section.Add(new MarkdownLine(MarkdownText.Bold("Value:")));
			}

			section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(member.ValueComments, member));
		}

		private static void AddRemarks(MarkdownSection section, DotNetMember member)
		{
			if(member.RemarksComments.Count == 0)
				return;

			section.Add(new MarkdownLine(MarkdownText.Bold("Remarks:")));
			section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(member.RemarksComments, member));
		}

		private static void AddReturns(MarkdownSection section, DotNetMember member)
		{
			if(member.ReturnsComments.IsEmpty)
				return;

			section.Add(new MarkdownLine(MarkdownText.Bold("Returns:")));
			section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(member.ReturnsComments, member));
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
				permissionSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(comment, member));
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
				section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(comment, member));
			}
		}

		private static void AddFloatingComments(MarkdownSection section, DotNetMember member)
		{
			if(member.FloatingComments.IsEmpty)
				return;

			section.Add(new MarkdownLine(MarkdownText.Bold("Misc:")));
			section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(member.FloatingComments, member));
		}

		internal static void AddTopLevelTypeParameters(MarkdownSection section, DotNetMember member)
		{
			if(member.TypeParameterComments.Count == 0)
				return;

			MarkdownSection parametersSection = new MarkdownSection("Generic Type Parameters");
			section.Add(parametersSection);

			if(EachCommentIsAllInlineComments(member.ParameterComments)) //todo: partially duplicated with AddParameters method
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
					section.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(commentParameter, member));
				}
			}
		}

		internal static void AddTypeParameters(MarkdownSection section, DotNetMember member)
		{
			if(member.TypeParameterComments.Count == 0)
				return;

			if(EachCommentIsAllInlineComments(member.ParameterComments))
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

		internal static void AddTopLevelParameters(MarkdownSection section, DotNetMethod method)
		{
			if(method.ParameterComments.Count == 0)
				return;

			MarkdownSection parametersSection = new MarkdownSection("Parameters");
			section.Add(parametersSection);

			if(EachCommentIsAllInlineComments(method.ParameterComments)) //todo: partially duplicated with AddParameters method
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
					parameterSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(commentParameter, method));
				}
			}
		}

		internal static void AddParameters(MarkdownSection section, DotNetMethod method)
		{
			if(method.ParameterComments.Count == 0)
				return;

			if(EachCommentIsAllInlineComments(method.ParameterComments))
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

		internal static void AddParameters(MarkdownSection section, DotNetIndexer indexer)
		{
			if(indexer.ParameterComments.Count == 0)
				return;

			if(EachCommentIsAllInlineComments(indexer.ParameterComments))
			{
				section.Add(new MarkdownLine(MarkdownText.Bold("Parameters:")));
				MarkdownList list = new MarkdownList(isNumbered: false);
				section.Add(list);
				foreach(DotNetParameter parameter in indexer.Parameters) //display in same order as in indexer signature
				{
					DotNetCommentParameter commentParameter = indexer.ParameterComments.FirstOrDefault(c => c.ParameterLink.Name == parameter.Name);
					if(commentParameter == null)
						continue;

					MarkdownLine line = new MarkdownLine(MarkdownText.Bold(parameter.ToHeader(indexer.Name.FullNamespace)), new MarkdownText(": "));
					line.Concat(ConvertDotNet.DotNetCommentGroupToMarkdownLine(commentParameter));
					list.Add(line);
				}
			}
			else
			{
				MarkdownSection parametersSection = section.AddSection("Parameters");
				foreach(DotNetParameter parameter in indexer.Parameters) //display in same order as in indexer signature; todo: repeated generator structure
				{
					DotNetCommentParameter commentParameter = indexer.ParameterComments.FirstOrDefault(c => c.ParameterLink.Name == parameter.Name);
					if(commentParameter == null)
						continue;

					MarkdownSection parameterSection = parametersSection.AddSection(parameter.ToHeader(indexer.Name.FullNamespace));
					AddGroupComments(parameterSection, commentParameter, indexer);
				}
			}
		}

		internal static void AddTopLevelExceptions(MarkdownSection section, DotNetMember member)
		{
			if(member.ExceptionComments.Count == 0)
				return;

			MarkdownSection exceptionsSection = new MarkdownSection("Exceptions");
			section.Add(exceptionsSection);

			if(EachCommentIsAllInlineComments(member.ExceptionComments)) //todo: partially duplicated with AddExceptions method
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
					exceptionSection.Add(ConvertDotNet.DotNetCommentGroupToMarkdown(comment, member));
				}
			}
		}

		private static void AddExceptions(MarkdownSection section, DotNetMember member)
		{
			if(member.ExceptionComments.Count == 0)
				return;

			if(EachCommentIsAllInlineComments(member.ExceptionComments))
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
					MarkdownSection exceptionSection = exceptionsSection.AddSection(comment.QualifiedLink.Name.ToDisplayStringLink(member.Name) + ":");
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
		/// Returns true if each comment boils down to just inline comments, or no comments at all.
		/// </summary>
		private static bool EachCommentIsAllInlineComments(List<DotNetCommentParameter> comments)
		{
			foreach(DotNetCommentParameter comment in comments)
			{
				if(!CommentIsAllInlineComments(comment as DotNetCommentGroup))
					return false;
			}
			return true;
		}

		/// <summary>
		/// Returns true if each comment boils down to just inline comments, or no comments at all.
		/// </summary>
		private static bool EachCommentIsAllInlineComments(List<DotNetCommentQualifiedLinkedGroup> comments)
		{
			foreach(DotNetCommentQualifiedLinkedGroup comment in comments)
			{
				if(!CommentIsAllInlineComments(comment as DotNetCommentGroup))
					return false;
			}
			return true;
		}

		/// <summary>
		/// Returns true if this whole comment boils down to just inline comments, or no comments at all.
		/// </summary>
		private static bool CommentIsAllInlineComments(DotNetCommentGroup group)
		{
			if(group.IsEmpty)
				return true;
			return group.Comments.All(c =>
				(c is DotNetCommentText || c is DotNetCommentCode || c is DotNetCommentParameterLink ||
				c is DotNetCommentTypeParameterLink) 
				&& 
				!(c is DotNetCommentCodeBlock)
			);
		}
	}
}
