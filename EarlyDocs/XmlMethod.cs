using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace EarlyDocs
{
	class XmlMethod : XmlMember
	{
		public string Signature { get; protected set; }
		public string Parameters { get; protected set; }
		public string ShortSignature { get { return Name + Parameters; } }
		public string TypeName { get; protected set; }
		public string Assembly { get; protected set; }
		public string Name { get; protected set; }
		public bool IsConstructor { get; protected set; }

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

		public string ToMarkdown(int indent)
		{
			StringBuilder output = new StringBuilder();

			output.Append(String.Format("{0} {1}\n\n", new String('#', indent), ShortSignature));
			if(!String.IsNullOrEmpty(Summary))
			{
				output.Append(String.Format("{0}\n\n", MarkdownSummary));
			}

			return output.ToString();
		}
	}
}
