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
		public string ReturnTypeName { get; protected set; }
		public bool IsConstructor { get; protected set; }
		public bool IsStatic { get; protected set; }
		public bool IsOperator { get; protected set; }

		public List<XmlParam> Params = new List<XmlParam>();
		public List<XElement> ParamsXml = new List<XElement>();

		public List<XmlException> Exceptions = new List<XmlException>();

		public XmlMethod(XElement element) : base(element)
		{
			Signature = element.Attribute("name")?.Value.Substring(2);
			ParseTypeNameAndAssembly(Signature);
			ParseMemberName(Signature);
			ParseParameters(element, Signature); //after TypeName

			IsConstructor = (Name == "#ctor");
			if(IsConstructor)
			{
				Name = TypeName.Substring(TypeName.LastIndexOf('.') + 1);
			}
			IsOperator = Name.StartsWith("op_");

			foreach(XElement child in element.Descendants())
			{
				if(child.Name == "exception")
				{
					Exceptions.Add(new XmlException(child));
				}
			}
		}

		private void ParseParameters(XElement element, string signature)
		{
			if(!signature.Contains("("))
			{
				Parameters = "()";
				return;
			}
			string parameters = signature.Substring(signature.IndexOf('('));
			parameters = parameters.Replace("(", "").Replace(")", "");
			string[] fields = parameters.Split(',');
			for(int i = 0; i < fields.Length; i++)
			{
				string f = fields[i];
				Params.Add(new XmlParam(f, Assembly));
				if(f.StartsWith(Assembly))
				{
					f = f.Substring(Assembly.Length + 1);
				}
				fields[i] = f;
			}
			Parameters = String.Format("({0})", String.Join(", ", fields));

			ParamsXml.AddRange(element.Descendants().Where(c => c.Name == "param"));
		}

		private void ParseTypeNameAndAssembly(string signature)
		{
			string fullName = signature;
			if(signature.Contains('('))
			{
				fullName = signature.Substring(0, signature.IndexOf('('));
			}
			string[] fields = fullName.Split('.');
			TypeName = String.Join(".", fields.Take(fields.Length - 1).ToArray());
			Assembly = String.Join(".", fields.Take(fields.Length - 2).ToArray());
		}

		private void ParseMemberName(string signature)
		{
			string fullName = signature;
			if(signature.Contains('('))
			{
				fullName = signature.Substring(0, signature.IndexOf('('));
			}
			string[] fields = fullName.Split('.');
			Name = fields.Last();
		}

		//todo: this parsing of the signature could be a lot better

		public bool MatchesSignature(MethodInfo methodInfo)
		{
			if(methodInfo.Name != Name)
				return false;
			return MatchesArguments(methodInfo.GetParameters());
		}

		public bool MatchesArguments(ParameterInfo[] parameterInfos)
		{
			if(Parameters == "()" && parameterInfos.Length == 0)
			{
				return true;
			}
			string[] parameters = Parameters.Replace("(", "").Replace(")", "").Split(',');
			if(parameters.Length != parameterInfos.Length)
				return false;

			for(int i = 0; i < parameters.Length; i++)
			{
				string parameter = parameters[i].Trim().LastTerm();
				if(parameter.EndsWith("@"))
				{
					parameter = parameter.Replace("@", "&");
				}
				if(parameter != parameterInfos[i].ParameterType.Name)
				{
					return false;
				}
			}
			return true;
		}

		public void Apply(MethodInfo methodInfo)
		{
			IsStatic = ((methodInfo.Attributes & STATIC_METHODATTRIBUTES) == STATIC_METHODATTRIBUTES);
			ReturnTypeName = methodInfo.ReturnType?.Name;

			int index = 0;
			foreach(ParameterInfo parameterInfo in methodInfo.GetParameters())
			{
				Params[index].Apply(parameterInfo);
				index++;
			}
			foreach(XElement element in ParamsXml)
			{
				XmlParam x = Params.FirstOrDefault(p => p.Name == element.Attribute("name").Value);
				if(x == null) continue;

				x.Apply(element);
			}
		}

		public void Apply(ConstructorInfo methodInfo)
		{
			int index = 0;
			foreach(ParameterInfo parameterInfo in methodInfo.GetParameters())
			{
				Params[index].Apply(parameterInfo);
				index++;
			}
			foreach(XElement element in ParamsXml)
			{
				XmlParam x = Params.FirstOrDefault(p => p.Name == element.Attribute("name").Value);
				if(x == null) continue;

				x.Apply(element);
			}
		}

		public string ToMarkdown(int indent)
		{
			StringBuilder output = new StringBuilder();

			if(IsOperator && operatorToSymbol.ContainsKey(Name))
			{
				string[] types = Parameters.Replace("(", "").Replace(")", "").Split(',');
				output.Append(String.Format("{0} {1} = ({2} {3} {4})\n\n", new String('#', indent), ReturnTypeName, types[0].Trim(), operatorToSymbol[Name], types[1].Trim()));
			}
			else
			{
				output.Append(String.Format("{0} {1}{2}{3}\n\n", new String('#', indent), ((ReturnTypeName != null) ? ReturnTypeName + " " : ""), Name, FormatParameters()));
			}

			if(!Summary.IsEmpty)
			{
				output.Append(String.Format("{0}\n\n", Summary));
			}

			bool displayedParameter = false;
			foreach(XmlParam p in Params.Where(p => !String.IsNullOrEmpty(p.Description)))
			{
				output.Append(String.Format("Parameter {0}: {1}  \n", p.Name, p.Description));
				displayedParameter = true;
			}
			if(displayedParameter)
			{
				output.Append("\n");
			}

			if(!Returns.IsEmpty)
			{
				output.Append(String.Format("Returns: {0}\n\n", Returns));
			}

			bool displayedException = false;
			foreach(XmlException e in Exceptions)
			{
				output.Append(String.Format("{0}: {1}\n", e.ExceptionType, e.Description));
				displayedException = true;
			}
			if(displayedException)
			{
				output.Append("\n");
			}

			return output.ToString();
		}

		private string FormatParameters()
		{
			return String.Format("({0})", String.Join(", ", Params.Select(p => p.ToMarkdown())));
		}
	}
}
