using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

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

				method.Apply(methodInfo);
			}

			foreach(ConstructorInfo constructorInfo in typeInfo.DeclaredConstructors)
			{
				XmlMethod method = Methods.FirstOrDefault(m => m.IsConstructor && m.MatchesArguments(constructorInfo.GetParameters()));
				if(method == null) continue;

				method.Apply(constructorInfo);
			}
			//todo: if public members are not already here, add them (but not automatic Property set/get methods)
			//so you don't have to put useless text on self-explanatory members
			//can I turn off the green underlines in Visual Studio?
		}

		public virtual string PreSummary()
		{
			if(IsStatic)
				return "Static type.\n\n";
			if(IsInterface)
				return "Interface type.\n\n";
			return null;
		}

		public virtual string ToMarkdown(int indent)
		{
			if(IsEnum) return EnumToMarkdown(indent);

			StringBuilder output = new StringBuilder();

			output.Append(String.Format("{0} {1}\n\n", new String('#', indent), Name));
			output.Append(PreSummary());
			output.Append(String.Format("{0}\n\n", Summary));
			if(!String.IsNullOrEmpty(BaseTypeName))
			{
				if(BaseNamespace == Assembly)
				{
					output.Append(String.Format("Base Type: [{0}]({0}.md)\n\n", BaseTypeName));
				}
				else
				{
					output.Append(String.Format("Base Type: {0}.{1}\n\n", BaseNamespace, BaseTypeName));
				}
			}

			if(Enums.Count > 0)
			{
				output.Append(String.Format("{0} Enums\n\n", new String('#', indent + 1)));
				foreach(XmlType e in Enums.OrderBy(m => m.Name))
				{
					output.Append(e.ToMarkdown(indent + 2));
				}
			}

			if(Fields.Count > 0)
			{
				output.Append(String.Format("{0} Fields\n\n", new String('#', indent + 1)));
				if(ConstantFields.Count > 0)
				{
					output.Append(String.Format("{0} Constant Fields\n\n", new String('#', indent + 2)));
					foreach(XmlField field in ConstantFields.OrderBy(m => m.Name))
					{
						output.Append(field.ToMarkdown(indent + 3));
					}
				}
				if(NormalFields.Count > 0)
				{
					output.Append(String.Format("{0} Normal Fields\n\n", new String('#', indent + 2)));
					foreach(XmlField field in NormalFields.OrderBy(m => m.Name))
					{
						output.Append(field.ToMarkdown(indent + 3));
					}
				}
			}

			if(Properties.Count > 0)
			{
				output.Append(String.Format("{0} Properties\n\n", new String('#', indent + 1)));
				foreach(XmlProperty p in Properties.OrderBy(m => m.Name))
				{
					output.Append(p.ToMarkdown(indent + 2));
				}
			}

			if(Constructors.Count > 0)
			{
				output.Append(String.Format("{0} Constructors\n\n", new String('#', indent + 1)));
				foreach(XmlMethod method in Constructors.OrderBy(m => m.Name))
				{
					output.Append(method.ToMarkdown(indent + 2));
				}
			}

			if(StaticMethods.Count > 0)
			{
				output.Append(String.Format("{0} Static Methods\n\n", new String('#', indent + 1)));
				foreach(XmlMethod method in StaticMethods.OrderBy(m => m.Name))
				{
					output.Append(method.ToMarkdown(indent + 2));
				}
			}

			if(NormalMethods.Count > 0)
			{
				output.Append(String.Format("{0} Methods\n\n", new String('#', indent + 1)));
				foreach(XmlMethod method in NormalMethods.OrderBy(m => m.Name))
				{
					output.Append(method.ToMarkdown(indent + 2));
				}
			}

			if(Operators.Count > 0)
			{
				output.Append(String.Format("{0} Operators\n\n", new String('#', indent + 1)));
				foreach(XmlMethod method in Operators.OrderBy(m => m.Name))
				{
					output.Append(method.ToMarkdown(indent + 2));
				}
			}

			if(NestedTypes.Count > 0)
			{
				output.Append(String.Format("{0} Nested Types\n\n", new String('#', indent + 1)));
				foreach(XmlType e in NestedTypes.OrderBy(m => m.Name))
				{
					output.Append(e.ToMarkdown(indent + 2));
				}
			}

			return output.ToString();
		}

		private string EnumToMarkdown(int indent)
		{
			StringBuilder output = new StringBuilder();

			output.Append(String.Format("{0} {1}\n\n", new String('#', indent), Name));
			output.Append(String.Format("{0}\n\n", Summary));

			if(Fields.Count > 0)
			{
				foreach(XmlField field in Fields)
				{
					if(field.Summary.IsEmpty)
					{
						output.Append(String.Format("* {0}  \n", field.Name));
					}
					else
					{
						output.Append(String.Format("* {0}: {1}  \n", field.Name, field.Summary));
					}
				}
			}

			output.Append("\n");

			return output.ToString();
		}
	}
}
