[EarlyDocs Home](README.md)

# Using EarlyDocs

EarlyDocs was tested on C# projects in Visual Studio. All instructions and examples are based on that setup.

## Installation

Use the NuGet Console or Manager to install EarlyDocs in your .Net project.

Search for "EarlyDocs" in the NuGet Manager,  
or run "Install-Package EarlyDocs" in the NuGet Console

[NuGet project page](https://www.nuget.org/packages/EarlyDocs/)

## Export XML Comments

To have your XML comments included in the generated documentation:

1. Open project properties > Build tab
2. Select the right configuration (Debug and/or release)
3. Check "XML documentation file" under the "Output" section

## Configuration

### Definitions

Properties and Items in the *.csproj and EarlyDocs.target files.

**EarlyDocsDllFile**: The full path and filename of the *.dll produced by your project.

**EarlyDocsXmlFile**: The full path and filename of the *.XML documentation file produced by Visual Studio.

**EarlyDocsOutputDir**: The full path to the "documentation" directory the Markdown files will be saved to.
* If the output directory does not exist, it will be created.

**EarlyDocsEmptyOutputDir**: If True, the **EarlyDocsOutputDir** will be emptied before new files are saved to it.
* Valid values are True and False

**EarlyDocsSkip**: If True, the entire EarlyDocs process will be skipped.
* Valid values are True and False

**EarlyDocsInclude**: A list of full path and filenames of third-party *.dll files that your project references.
* Only include a library here if your project's public interface returns or accepts Types from this third-party library.
* If you do not include one of these libraries, all those Type names will be missing from your documentation.

### Defaults

**EarlyDocsDllFile**: $(ProjectDir)$(OutDir)$(ProjectName).dll

**EarlyDocsXmlFile**: $(ProjectDir)$(OutDir)$(ProjectName).XML

**EarlyDocsOutputDir**: $(ProjectDir)$(OutDir)documentation\

**EarlyDocsEmptyOutputDir**: True

**EarlyDocsSkip**: False

**EarlyDocsInclude**: no values

### Overriding Defaults

1) Open your *.csproj file in a text editor.

2) Locate the line `<Import Project="$(MSBuildToolsPath)\Microsoft.CSharp.targets" />`.
* This line sets the values of most MSBuild macros, such as $(ProjectDir) and $(OutDir).
* If you want to use macros in your override values, then insert the following lines directly after this line.
* If you don't need macros, you can insert the override values higher in the file.

3) Override values

**EarlyDocsInclude** is a list of values, so add one element for each filename. The rest of the values are single properties.

```
  <PropertyGroup>
    <EarlyDocsDllFile>$(ProjectDir)$(OutDir)DifferentProjectName.dll</EarlyDocsDllFile>
    <EarlyDocsXmlFile>$(ProjectDir)$(OutDir)DifferentProjectName.XML</EarlyDocsXmlFile>
    <EarlyDocsOutputDir>$(ProjectDir)..\documentation\</EarlyDocsOutputDir>
    <EarlyDocsEmptyOutputDir>False</EarlyDocsEmptyOutputDir>
    <!--<EarlyDocsSkip>True</EarlyDocsSkip>-->
  </PropertyGroup>
  <ItemGroup>
    <EarlyDocsInclude Include="$(ProjectDir)$(OutDir)EPPlus.dll" />
    <EarlyDocsInclude Include="$(ProjectDir)$(OutDir)Markdown.dll" />
  </ItemGroup>
```

## Output

All Markdown files will be saved to the **EarlyDocsOutputDir** location.

Output:
* TableOfContents.md: a global table of contents that links to each namespace-table-of-contents and each type's page.
* TableOfContents.Namespace.md: one detailed table of contents for each namespace.
* Namespace.Type.md: one detailed documentation page for each type (class, interface, struct, enum, global delegate).
  * Generic-type filenames are saved as Namespace.Type_T_.md
  
Links:
* All namespaces link back to their parent namespace's table of contents.
* All types link back to their namespace's table of contents.
* All core .Net types link to the Microsoft documentation page for that type.

## Supported XML Tags

Supports all standard Microsoft XML tags. See examples in [How to Use XML Comments in .Net](HowToUseXmlComments.md).

### inheritdoc Custom Tag

Supports custom tag `<inheritdoc />` as a top-level tag.

The entire comments of the member/type with `<inheritdoc />` on it will be replaced with the entire comments of the member/type it inherits from.

Supported inheritance: 
* Classes inheriting from classes
* Classes and interfaces inheriting from interfaces
* Members inheriting from explicit interfaces

Example:  
TypeB will have the same documentation as TypeA.  
TypeB.MethodA will have the same documentation as TypeA.MethodA.  
```
/// <summary>
/// Summary of TypeA
/// </summary>
public class TypeA
{
	/// <summary>
	/// Summary of MethodA
	/// </summary>
	public virtual void MethodA() { }
}

/// <inheritdoc />
public class TypeB : TypeA
{
	/// <inheritdoc />
	public override void MethodA() { }
}
```

### duplicate Custom Tag

Supports custom tag `<duplicate cref="" />` as a top-level tag.

The entire comments of the member/type with `<duplicate cref="" />` on it will be replaced with the entire comments of the member/type being referenced.

Example:  
All three overloaded methods will have the same documentation.  
```
public class TypeA
{
	/// <summary>
	/// Summary of MethodA
	/// </summary>
	public void MethodA(int a) { }

	/// <duplicate cref="MethodA(int)" />
	public void MethodA(float a) { }
	
	/// <duplicate cref="MethodA(int)" />
	public void MethodA(double a) { }
}
```

[How to cref almost anything in your code.](HowToUseXmlComments.md#cref-attribute)

## Handling Errors

Errors in EarlyDocs will not affect your build. If an error occurs, it will be displayed as a Warning and the build process will complete.
