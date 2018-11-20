using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace EarlyDocs
{
	public class RunEarlyDocs : Task
	{
		/// <summary>
		/// Full path and filename of XML documentation file.
		/// </summary>
		public string LocationXML { get; set; }

		/// <summary>
		/// Full path and filename of DLL assembly file.
		/// </summary>
		public string LocationDLL { get; set; }

		/// <summary>
		/// Full path of output directory, where the new documentation will be saved.
		/// </summary>
		public string OutputDirectory { get; set; }

		/// <summary>
		/// Output property: set to 1 on success and 0 on an error.
		/// </summary>
		[Output]
		public int ReturnValue { get; set; }

		/// <summary>
		/// Output property: if an error occurs in the task, it will be placed here.
		/// </summary>
		[Output]
		public string RuntimeException { get; set; }

		/// <summary>
		/// Load documentation data from XML and DLL file. Output Markdown documentation to Output directory.
		/// </summary>
		/// <returns></returns>
		public override bool Execute()
		{
			try
			{
				new ConvertXML(LocationDLL, LocationXML, OutputDirectory);
			}
			catch(Exception e)
			{
				RuntimeException = e.ToString();
				ReturnValue = 0;
				return false;
			}

			ReturnValue = 1;
			return true;
		}
	}
}
