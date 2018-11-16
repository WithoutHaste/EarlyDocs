using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarlyDocs
{
	class Program
	{
		static int Main(string[] args)
		{
			if(args.Length != 3)
			{
				Console.WriteLine("EarlyDocs expects arguments: full path to *.dll, full path to *.XML, full path to output directory.");
				Environment.Exit(-1);
			}

			new ConvertXML(dllFilename: args[0], xmlDocumentationFilename: args[1], outputDirectory: args[2]);
			
			//Console.WriteLine("EarlyDocs completed.");
			//Console.ReadLine();

			return 0;
		}
	}
}
