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
			//new ConvertXML(
			//	"E:/Github/WithoutHaste.Drawing.Shapes/Shapes/bin/Release/WithoutHaste.Drawing.Shapes.dll",
			//	"E:/Github/WithoutHaste.Drawing.Shapes/Shapes/bin/Release/WithoutHaste.Drawing.Shapes.XML",
			//	"E:/Github/WithoutHaste.Drawing.Shapes/documentation"
			//);

			new ConvertXML(
				"E:/Github/WithoutHaste.Drawing.Colors/Colors/bin/Release/WithoutHaste.Drawing.Colors.dll",
				"E:/Github/WithoutHaste.Drawing.Colors/Colors/bin/Release/WithoutHaste.Drawing.Colors.XML",
				"E:/Github/WithoutHaste.Drawing.Colors/documentation"
			);

			Console.WriteLine("done");
			Console.ReadLine();
		}
	}
}
