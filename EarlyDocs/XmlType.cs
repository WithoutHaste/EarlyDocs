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

		public string TypeName { get; protected set; }
		public string Assembly { get; protected set; }
		public string Name { get; protected set; }
		public bool IsStatic { get; protected set; }

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
		public List<XmlEnum> Enums {
			get {
				return Types.OfType<XmlEnum>().ToList();
			}
		}

		public XmlType(XElement element) : base(element)
		{
			TypeName = element.Attribute("name")?.Value.Substring(2);
			ParseAssembly(TypeName);
			ParseName(TypeName);
		}

		public static XmlType Factory(XElement element)
		{
			if(element.Descendants().Any(d => d.Name == "enum"))
			{
				return new XmlEnum(element);
			}
			if(element.Descendants().Any(d => d.Name == "interface"))
			{
				return new XmlInterface(element);
			}
			return new XmlType(element);
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
		}

		public virtual string PreSummary()
		{
			if(IsStatic)
				return "Static type.\n\n";
			return null;
		}

		public virtual string ToMarkdown(int indent)
		{
			StringBuilder output = new StringBuilder();

			output.Append(String.Format("{0} {1}\n\n", new String('#', indent), Name));
			output.Append(PreSummary());
			output.Append(String.Format("{0}\n\n", Summary));

			if(Enums.Count > 0)
			{
				output.Append(String.Format("{0} Enums\n\n", new String('#', indent + 1)));
				foreach(XmlEnum e in Enums.OrderBy(m => m.Name))
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

			return output.ToString();
		}
	}
}
