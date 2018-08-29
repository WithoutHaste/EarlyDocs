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
		private readonly TypeAttributes STATIC_TYPEATTRIBUTES = TypeAttributes.Abstract | TypeAttributes.Sealed;
		private readonly TypeAttributes INTERFACE_TYPEATTRIBUTES = TypeAttributes.Abstract | TypeAttributes.ClassSemanticsMask;

		public string TypeName { get; protected set; }
		public string Assembly { get; protected set; }
		public string Name { get; protected set; }
		public bool IsStatic { get; protected set; }
		public bool IsInterface { get; protected set; }
		public bool IsEnum { get; protected set; }

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
			ParseAssembly(TypeName);
			ParseName(TypeName);
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

		public void Apply(TypeInfo typeInfo)
		{
			IsStatic = ((typeInfo.Attributes & STATIC_TYPEATTRIBUTES) == STATIC_TYPEATTRIBUTES);
			IsInterface = ((typeInfo.Attributes & INTERFACE_TYPEATTRIBUTES) == INTERFACE_TYPEATTRIBUTES);
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

			if(Methods.Count > 0)
			{
				output.Append(String.Format("{0} Methods\n\n", new String('#', indent + 1)));
				foreach(XmlMethod method in Methods.OrderBy(m => m.Name))
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
					output.Append(String.Format("* {0}: {1}  \n", field.Name, field.Summary));
				}
			}

			output.Append("\n");

			return output.ToString();
		}
	}
}
