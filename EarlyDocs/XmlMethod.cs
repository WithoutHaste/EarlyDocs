using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EarlyDocs
{
	class XmlMethod : XmlMember
	{
		private readonly MethodAttributes STATIC_METHODATTRIBUTES = MethodAttributes.Static;

		private readonly Dictionary<string, string> operatorToSymbol = new Dictionary<string, string>() {
			{ "op_Addition", "+" },
			{ "op_Substraction", "-" },
			{ "op_Multiply", "*" },
			{ "op_Division", "/" },
			{ "op_BitwiseAnd", "&" },
			{ "op_BitwiseOr", "|" },
			{ "op_LogicalAnd", "&&" },
			{ "op_LogicalOr", "||" },
			{ "op_Equality", "==" },
			{ "op_GreaterThan", ">" },
			{ "op_LessThan", "<" },
			{ "op_Inequality", "!=" },
			{ "op_GreaterThanOrEqual", ">=" },
			{ "op_LessThanOrEqual", "<=" },
			{ "op_AdditionAssignment", "+=" },
			{ "op_SubstractionAssignment", "-=" },
			{ "op_MultiplyAssignment", "*=" },
			{ "op_DivisionAssignment", "/=" },
			{ "op_BitwiseAndAssignment", "&=" },
			{ "op_BitwiseOrAssignment", "|=" },
			{ "op_Decrement", "--" },
			{ "op_Increment", "++" },
			/*
todo:
op_Implicit
op_Explicit
op_Modulus
op_ExclusiveOr
op_Assign
op_LeftShift
op_RightShift
op_SignedRightShift
op_UnsignedRightShift
op_ExclusiveOrAssignment
op_LeftShiftAssignment
op_ModulusAssignment
op_Comma
op_UnaryNegation
op_UnaryPlus
op_OnesComplement
*/
		};

		public string Signature { get; protected set; }
		public string Parameters { get; protected set; }
		public string ShortSignature { get { return Name + Parameters; } }
		public string TypeName { get; protected set; }
		public string Assembly { get; protected set; }
		public string Name { get; protected set; }
		public bool IsConstructor { get; protected set; }
		public bool IsStatic { get; protected set; }
		public bool IsOperator { get; protected set; }

		public XmlMethod(XElement element) : base(element)
		{
			Signature = element.Attribute("name")?.Value.Substring(2);
			ParseTypeNameAndAssembly(Signature);
			ParseMemberName(Signature);
			ParseParameters(Signature); //after TypeName

			IsConstructor = (Name == "#ctor");
			if(IsConstructor)
			{
				Name = TypeName.Substring(TypeName.LastIndexOf('.') + 1);
			}
			IsOperator = Name.StartsWith("op_");
		}

		private void ParseParameters(string signature)
		{
			string parameters = signature.Substring(signature.IndexOf('('));
			parameters = parameters.Replace("(", "").Replace(")", "");
			string[] fields = parameters.Split(',');
			for(int i = 0; i < fields.Length; i++)
			{
				string f = fields[i];
				if(f.StartsWith(Assembly))
				{
					f = f.Substring(Assembly.Length + 1);
				}
				fields[i] = f;
			}
			Parameters = String.Format("({0})", String.Join(", ", fields));
		}

		private void ParseTypeNameAndAssembly(string signature)
		{
			string fullName = signature.Substring(0, signature.IndexOf('('));
			string[] fields = fullName.Split('.');
			TypeName = String.Join(".", fields.Take(fields.Length - 1).ToArray());
			Assembly = String.Join(".", fields.Take(fields.Length - 2).ToArray());
		}

		private void ParseMemberName(string signature)
		{
			string fullName = signature.Substring(0, signature.IndexOf('('));
			string[] fields = fullName.Split('.');
			Name = fields.Last();
		}

		public void Apply(MethodInfo methodInfo)
		{
			IsStatic = ((methodInfo.Attributes & STATIC_METHODATTRIBUTES) == STATIC_METHODATTRIBUTES);
		}

		public string ToMarkdown(int indent)
		{
			StringBuilder output = new StringBuilder();

			if(IsOperator && operatorToSymbol.ContainsKey(Name))
			{
				string[] types = Parameters.Replace("(", "").Replace(")", "").Split(',');
				output.Append(String.Format("{0} {1} {2} {3}\n\n", new String('#', indent), types[0].Trim(), operatorToSymbol[Name], types[1].Trim()));
			}
			else
			{
				output.Append(String.Format("{0} {1}\n\n", new String('#', indent), ShortSignature));
			}

			if(!String.IsNullOrEmpty(Summary))
			{
				output.Append(String.Format("{0}\n\n", MarkdownSummary));
			}
			if(!String.IsNullOrEmpty(Returns))
			{
				output.Append(String.Format("Returns: {0}\n\n", Returns));
			}

			return output.ToString();
		}
	}
}
