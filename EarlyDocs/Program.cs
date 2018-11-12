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
				"../../../Test/bin/Debug/Test.dll",
				"../../../Test/bin/Debug/Test.XML",
				"../../../Test/documentation"
			);

			//new ConvertXML(
			//	"E:/Github/WithoutHaste.DataFiles/DataFiles/bin/Release/WithoutHaste.DataFiles.dll",
			//	"E:/Github/WithoutHaste.DataFiles/DataFiles/bin/Release/WithoutHaste.DataFiles.XML",
			//	"E:/Github/WithoutHaste.DataFiles/documentation"
			//);

			//new ConvertXML(
			//	"E:/Github/WithoutHaste.Drawing.Shapes/Shapes/bin/Release/WithoutHaste.Drawing.Shapes.dll",
			//	"E:/Github/WithoutHaste.Drawing.Shapes/Shapes/bin/Release/WithoutHaste.Drawing.Shapes.XML",
			//	"E:/Github/WithoutHaste.Drawing.Shapes/documentation"
			//);

			//new ConvertXML(
			//	"E:/Github/WithoutHaste.Drawing.Colors/Colors/bin/Release/WithoutHaste.Drawing.Colors.dll",
			//	"E:/Github/WithoutHaste.Drawing.Colors/Colors/bin/Release/WithoutHaste.Drawing.Colors.XML",
			//	"E:/Github/WithoutHaste.Drawing.Colors/documentation"
			//);

			//new ConvertXML(
			//	"E:/Github/WithoutHaste.Windows.GUI/GUI/bin/Release/WithoutHaste.Windows.GUI.dll",
			//	"E:/Github/WithoutHaste.Windows.GUI/GUI/bin/Release/WithoutHaste.Windows.GUI.XML",
			//	"E:/Github/WithoutHaste.Windows.GUI/documentation"
			//);

			Console.WriteLine("done");
			Console.ReadLine();
		}
	}
}
