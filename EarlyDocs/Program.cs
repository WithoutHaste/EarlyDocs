using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EarlyDocs
{
	class Program
	{
		static void Main(string[] args)
		{
			new ConvertXML(
				"E:/Github/WithoutHaste.Drawing.Shapes/Shapes/bin/Release/WithoutHaste.Drawing.Shapes.XML",
				"E:/Github/WithoutHaste.Drawing.Shapes/documentation"
			);

			Console.WriteLine("done");
			Console.ReadLine();
		}
	}
}
