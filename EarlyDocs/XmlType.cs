using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using WithoutHaste.DataFiles.Markdown;

namespace EarlyDocs
{
	class XmlType : XmlMember
	{
		public string BaseNamespace { get; protected set; }
		public string BaseTypeName { get; protected set; }

		public string TypeName { get; protected set; }
		public string Assembly { get; protected set; }
		public string Name { get; protected set; }
		public bool IsAbstract { get; protected set; }
		public bool IsStatic { get; protected set; }
		public bool IsInterface { get; protected set; }
		public bool IsEnum { get; protected set; }
		public bool IsException { get; protected set; }

		public bool LayoutKeepMethodOrder { get; protected set; }

		public List<XmlField> Fields = new List<XmlField>();
		public List<XmlField> ConstantFields {
			get {
				return Fields.Where(f => f.IsConstant).ToList();
			}
		}
		public List<XmlField> NormalFields {
			get {
				return Fields.Where(f => !f.IsConstant).ToList();
			}
		}

		public List<XmlProperty> Properties = new List<XmlProperty>();
		public List<XmlEvent> Events = new List<XmlEvent>();
		
		public List<XmlMethod> Methods = new List<XmlMethod>();
		public List<XmlMethod> NormalMethods{
			get {
				return Methods.Where(t => !t.IsConstructor && !t.IsStatic && !t.IsOperator).ToList();
			}
		}
		public List<XmlMethod> Constructors {
			get {
				return Methods.Where(t => t.IsConstructor).ToList();
			}
		}
		public List<XmlMethod> StaticMethods {
			get {
				return Methods.Where(t => t.IsStatic && !t.IsOperator).ToList();
			}
		}
		public List<XmlMethod> Operators {
			get {
				return Methods.Where(t => t.IsOperator).ToList();
			}
		}

		public List<XmlType> Types = new List<XmlType>();
		public List<XmlType> NestedTypes {
			get {
				return Types.Where(t => !t.IsEnum).ToList();
			}
		}
		public List<XmlType> Enums {
			get {
				return Types.Where(t => t.IsEnum).ToList();
			}
		}

		public XmlType(XElement element) : base(element)
		{
			TypeName = element.Attribute("name")?.Value.Substring(2);
			if(TypeName.Contains("`"))
			{
				TypeName = TypeName.Substring(0, TypeName.IndexOf('`')); //this is due to generic types
			}
			ParseAssembly(TypeName);
			ParseName(TypeName);
			IsException = Name.EndsWith("Exception");

			XElement layout = element.Descendants().FirstOrDefault(d => d.Name == "layout");
			if(layout != null)
			{
				if(layout.Attribute("methods") != null)
				{
					LayoutKeepMethodOrder = (layout.Attribute("methods").Value == "keep_order");
				}
			}
		}

		private void ParseAssembly(string fullName)
		{
			string[] fields = fullName.Split('.');
			Assembly = String.Join(".", fields.Take(fields.Length - 1).ToArray());
		}

		private void ParseName(string fullName)
		{
			string[] fields = fullName.Split('.');
			Name = fields.Last();
		}

		public void Add(XmlField field)
		{
			Fields.Add(field);
		}

		public void Add(XmlProperty property)
		{
			Properties.Add(property);
		}

		public void Add(XmlEvent e)
		{
			Events.Add(e);
		}

		public void Add(XmlMethod method)
		{
			Methods.Add(method);
		}

		public void Add(XmlType type)
		{
			Types.Add(type);
		}

		private bool TypeIsAbstract(TypeAttributes attributes)
		{
			if((attributes & TypeAttributes.Sealed) == TypeAttributes.Sealed)
				return false;
			if((attributes & TypeAttributes.ClassSemanticsMask) == TypeAttributes.ClassSemanticsMask)
				return false;
			return ((attributes & TypeAttributes.Abstract) == TypeAttributes.Abstract);
		}

		private bool TypeIsStatic(TypeAttributes attributes)
		{
			if((attributes & TypeAttributes.Sealed) != TypeAttributes.Sealed)
				return false;
			return ((attributes & TypeAttributes.Abstract) == TypeAttributes.Abstract);
		}

		private bool TypeIsInterface(TypeAttributes attributes)
		{
			if((attributes & TypeAttributes.ClassSemanticsMask) != TypeAttributes.ClassSemanticsMask)
				return false;
			return ((attributes & TypeAttributes.Abstract) == TypeAttributes.Abstract);
		}

		public void Apply(TypeInfo typeInfo)
		{
			BaseNamespace = typeInfo.BaseType?.Namespace;
			BaseTypeName = typeInfo.BaseType?.Name;
			IsAbstract = TypeIsAbstract(typeInfo.Attributes);
			IsStatic = TypeIsStatic(typeInfo.Attributes);
			IsInterface = TypeIsInterface(typeInfo.Attributes);
			IsEnum = (typeInfo.BaseType != null && typeInfo.BaseType.Name == "Enum");

			foreach(FieldInfo fieldInfo in typeInfo.DeclaredFields)
			{
				XmlField field = Fields.FirstOrDefault(f => fieldInfo.Name == f.Name);
				if(field == null) continue;

				field.Apply(fieldInfo);
			}

			foreach(PropertyInfo propertyInfo in typeInfo.DeclaredProperties)
			{
				XmlProperty property = Properties.FirstOrDefault(p => propertyInfo.Name == p.Name);
				if(property == null) continue;

				property.Apply(propertyInfo);
			}

			foreach(MethodInfo methodInfo in typeInfo.DeclaredMethods)
			{
				XmlMethod method = Methods.FirstOrDefault(m => m.MatchesSignature(methodInfo));
				if(method == null) continue;

				if((methodInfo.Attributes & MethodAttributes.Private) == MethodAttributes.Private)
				{
					Methods.Remove(method);
					continue;
				}

				method.Apply(methodInfo);
			}

			foreach(ConstructorInfo constructorInfo in typeInfo.DeclaredConstructors)
			{
				XmlMethod method = Methods.FirstOrDefault(m => m.IsConstructor && m.MatchesArguments(constructorInfo.GetParameters()));
				if(method == null) continue;

				method.Apply(constructorInfo);
			}

			foreach(EventInfo eventInfo in typeInfo.DeclaredEvents)
			{
				XmlEvent e = Events.FirstOrDefault(m => m.Name == eventInfo.Name);
				if(e == null) continue;

				e.Apply(eventInfo);
			}
		}

		public virtual string PreSummary()
		{
			if(IsStatic)
				return "Static type.\n\n";
			if(IsInterface)
				return "Interface type.\n\n";
			return null;
		}

		public virtual MarkdownFile ToMarkdownFile()
		{
			MarkdownFile markdown = new MarkdownFile();

			if(IsEnum)
			{
				markdown.AddSection(EnumToMarkdownSection());
			}
			else
			{
				markdown.AddSection(ToMarkdownSection());
			}

			return markdown;
		}

		public virtual MarkdownSection ToMarkdownSection()
		{
			MarkdownSection typeSection = new MarkdownSection(Name);
			typeSection.AddInParagraph(PreSummary());
			if(!Summary.IsEmpty)
			{
				typeSection.AddInParagraph(Summary.ToString());
			}
			if(!Remarks.IsEmpty)
			{
				typeSection.AddInParagraph(Remarks.ToString());
			}
			if(!String.IsNullOrEmpty(BaseTypeName))
			{
				if(BaseNamespace == Assembly)
				{
					typeSection.Add(new MarkdownParagraph(new MarkdownText("Base Type: "), new MarkdownInlineLink(BaseTypeName, BaseTypeName + Ext.MD)));
				}
				else
				{
					typeSection.AddInParagraph(String.Format("Base Type: {0}.{1}", BaseNamespace, BaseTypeName));
				}
			}
			if(Examples.Count > 0)
			{
				MarkdownSection exampleSection = typeSection.AddSection("Examples");
				foreach(XmlComments c in Examples)
				{
					exampleSection.AddInParagraph(c.ToString());
				}
			}
			if(Enums.Count > 0)
			{
				MarkdownSection enumSection = typeSection.AddSection("Enums");
				foreach(XmlType e in Enums.OrderBy(m => m.Name))
				{
					enumSection.AddSection(e.EnumToMarkdownSection());
				}
			}
			if(Fields.Count > 0)
			{
				MarkdownSection fieldSection = typeSection.AddSection("Fields");
				if(ConstantFields.Count > 0)
				{
					MarkdownSection constantFieldSection = fieldSection.AddSection("Constant Fields");
					foreach(XmlField field in ConstantFields.OrderBy(m => m.Name))
					{
						constantFieldSection.AddInParagraph(field.ToMarkdown(constantFieldSection.Depth + 1));
					}
				}
				if(NormalFields.Count > 0)
				{
					MarkdownSection normalFieldSection = fieldSection.AddSection("Normal Fields");
					foreach(XmlField field in NormalFields.OrderBy(m => m.Name))
					{
						normalFieldSection.AddInParagraph(field.ToMarkdown(normalFieldSection.Depth + 1));
					}
				}
			}
			if(Properties.Count > 0)
			{
				MarkdownSection propertySection = typeSection.AddSection("Properties");
				foreach(XmlProperty p in Properties.OrderBy(m => m.Name))
				{
					propertySection.AddInParagraph(p.ToMarkdown(propertySection.Depth + 1));
				}
			}
			if(Events.Count > 0)
			{
				MarkdownSection eventSection = typeSection.AddSection("Events");
				foreach(XmlEvent e in Events.OrderBy(m => m.Name))
				{
					eventSection.AddInParagraph(e.ToMarkdown(eventSection.Depth + 1));
				}
			}

			MethodsToMarkdown(typeSection, "Constructors", Constructors);
			MethodsToMarkdown(typeSection, "Static Methods", StaticMethods);
			MethodsToMarkdown(typeSection, "Methods", NormalMethods);

			if(Operators.Count > 0)
			{
				MarkdownSection operatorSection = typeSection.AddSection("Operators");
				foreach(XmlMethod method in Operators.OrderBy(m => m.Name))
				{
					operatorSection.AddInParagraph(method.ToMarkdown(operatorSection.Depth + 1));
				}
			}
			if(NestedTypes.Count > 0)
			{
				MarkdownSection nestedTypeSection = typeSection.AddSection("Nested Types");
				foreach(XmlType e in NestedTypes.OrderBy(m => m.Name))
				{
					nestedTypeSection.AddSection(e.ToMarkdownSection());
				}
			}

			return typeSection;
		}

		private void MethodsToMarkdown(MarkdownSection parent, string header, List<XmlMethod> methods)
		{
			if(methods.Count == 0) return;

			MarkdownSection methodSection = parent.AddSection(header);
			if(!LayoutKeepMethodOrder)
			{
				methods = methods.OrderBy(m => m.Name).ToList();
			}
			foreach(XmlMethod method in methods)
			{
				methodSection.AddInParagraph(method.ToMarkdown(methodSection.Depth + 1));
			}
		}

		private MarkdownSection EnumToMarkdownSection()
		{
			MarkdownSection enumSection = new MarkdownSection(Name);

			if(!Summary.IsEmpty)
			{
				enumSection.AddInParagraph(Summary.ToString());
			}
			if(!Remarks.IsEmpty)
			{
				enumSection.AddInParagraph(Remarks.ToString());
			}
			if(Examples.Count > 0)
			{
				MarkdownSection exampleSection = enumSection.AddSection("Examples");
				foreach(XmlComments c in Examples)
				{
					exampleSection.AddInParagraph(c.ToString());
				}
			}
			if(Fields.Count > 0)
			{
				MarkdownSection fieldSection = enumSection.AddSection("Constants");
				MarkdownList list = new MarkdownList(isOrdered: false);
				fieldSection.Add(list);

				foreach(XmlField field in Fields)
				{
					if(field.Summary.IsEmpty)
					{
						list.Add(new MarkdownText(field.Name));
					}
					else
					{
						list.Add(new MarkdownText(String.Format("{0}: {1}", field.Name, field.Summary.ToString())));
						if(field.Examples.Count > 0)
						{
							MarkdownList exampleList = new MarkdownList();
							list.Add(exampleList);
							foreach(XmlComments example in field.Examples)
							{
								exampleList.Add(new MarkdownText("Example: " + example.ToString()));
							}
						}
					}
				}
			}

			return enumSection;
		}
	}
}
