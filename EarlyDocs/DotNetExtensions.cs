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
		public static string ToDisplayString(this DotNetParameter parameter)
		{
			if(parameter.TypeName == null)
				return parameter.Name;
			if(String.IsNullOrEmpty(parameter.Name))
				return parameter.TypeName.ToDisplayString();
			return parameter.TypeName.ToDisplayString() + " " + parameter.Name;
		}

		public static string ToDisplayString(this DotNetQualifiedName name)
		{
			switch(name.FullName)
			{
				case "System.Double": return "double";
				case "System.Float": return "float";
				case "System.Int16": return "short";
				case "System.Int32": return "int";
				case "System.Int64": return "long";
				case "System.String": return "string";
			}
			switch(name.FullNamespace)
			{
				case "System.Collections.Generic": return name.LocalName;
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
				MarkdownSection exampleSection = typeSection.AddSection("Examples");
				exampleSection.Add(ConvertDotNet.DotNetCommentsToMarkdown(type.ExampleComments));
			}
			if(type.NestedEnums.Count > 0)
			{
				MarkdownSection enumSection = typeSection.AddSection("Enums");
				foreach(DotNetType e in type.NestedEnums.OrderBy(m => m.Name.LocalName))
				{
					enumSection.AddSection(e.ToMarkdownEnumSection());
				}
			}
			if(type.Fields.Count > 0)
			{
				MarkdownSection fieldSection = typeSection.AddSection("Fields");
				if(type.ConstantFields.Count > 0)
				{
					MarkdownSection constantFieldSection = fieldSection.AddSection("Constant Fields");
					foreach(DotNetField field in type.ConstantFields.OrderBy(m => m.Name.LocalName))
					{
						constantFieldSection.AddSection(ToMarkdownSection(field));
					}
				}
				if(type.NormalFields.Count > 0)
				{
					MarkdownSection normalFieldSection = fieldSection.AddSection("Normal Fields");
					foreach(DotNetField field in type.NormalFields.OrderBy(m => m.Name.LocalName))
					{
						normalFieldSection.AddSection(ToMarkdownSection(field));
					}
				}
			}
			if(type.Properties.Count > 0)
			{
				MarkdownSection propertySection = typeSection.AddSection("Properties");
				foreach(DotNetProperty p in type.Properties.OrderBy(m => m.Name.LocalName))
				{
					propertySection.AddSection(ToMarkdownSection(p as DotNetField));
				}
			}
			if(type.Events.Count > 0)
			{
				MarkdownSection eventSection = typeSection.AddSection("Events");
				foreach(DotNetEvent e in type.Events.OrderBy(m => m.Name.LocalName))
				{
					eventSection.AddSection(ToMarkdownSection(e as DotNetField));
				}
			}
			
			MethodsToMarkdown(typeSection, "Constructors", type.ConstructorMethods.Cast<DotNetMethod>().ToList());
			MethodsToMarkdown(typeSection, "Static Methods", type.StaticMethods);
			MethodsToMarkdown(typeSection, "Methods", type.NormalMethods);
			MethodsToMarkdown(typeSection, "Operators", type.OperatorMethods.Cast<DotNetMethod>().ToList());
			/*
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
			string header = field.Name.LocalName;
			if(field.TypeName != null)
				header += " " + field.TypeName.ToDisplayString();
			MarkdownSection memberSection = new MarkdownSection(header);

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
				MarkdownSection exampleSection = memberSection.AddSection("Examples");
				exampleSection.Add(ConvertDotNet.DotNetCommentsToMarkdown(field.ExampleComments));
			}
			//todo: value tag

			return memberSection;
		}

		public static MarkdownSection ToMarkdownSection(this DotNetMethod method)
		{
			string header = method.Name.LocalName;
			if(method.Parameters == null || method.Parameters.Count == 0)
				header += "()";
			else
				header += String.Format("({0})", String.Join(", ", method.Parameters.Select(p => p.ToDisplayString()).ToArray()));
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
			if(method.ExampleComments.Count > 0)
			{
				//todo: label examples as A, B, C...
				MarkdownSection exampleSection = memberSection.AddSection("Examples");
				exampleSection.Add(ConvertDotNet.DotNetCommentsToMarkdown(method.ExampleComments));
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

		private static void MethodsToMarkdown(MarkdownSection parent, string header, List<DotNetMethod> methods)
		{
			if(methods.Count == 0) return;

			MarkdownSection methodSection = parent.AddSection(header);
			methods = methods.OrderBy(m => m.Name.LocalName).ToList();
			foreach(DotNetMethod method in methods)
			{
				methodSection.AddSection(ToMarkdownSection(method));
			}
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
