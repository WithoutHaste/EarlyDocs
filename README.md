# EarlyDocs

EarlyDocs generates Markdown documentation for your .Net library.

Install the EarlyDocs NuGet package to insert EarlyDocs into your build process. It will run after your project build is complete. Errors in EarlyDocs will not affect your build.

Summary:
* Loads documentation from your Project.dll and Project.XML files.
* Generates one Markdown documentation page for each Type in your project.
* Generates one global Table of Contents per project and one detailed Table of Contents per namespace.
* All Markdown files are inter-linked.
* Everything is saved to one "documentation" directory.

[Using EarlyDocs](USING_EARLYDOCS.md)

EarlyDocs is build on top of 
* [WithoutHaste.DataFiles.DotNet](https://github.com/WithoutHaste/WithoutHaste.DataFiles/tree/master/DataFiles/DotNet) which handles all the loading of information from the dll and xml files into an object model.
* [WithoutHaste.DataFiles.Markdown](https://github.com/WithoutHaste/WithoutHaste.DataFiles/tree/master/DataFiles/Markdown) for building the Markdown files.

This library is under active development. Report bugs and request features on Github, or to wohaste@gmail.com.

## Supported XML Tags

Supports all standard Microsoft XML tags.

See examples: [How to Use XML Comments in .Net](HowToUseXmlComments.md)

### inheritdoc

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

### duplicate

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

## Examples

Projects with EarlyDocs-generated documentation.

[WithoutHaste.Drawing.Colors](https://github.com/WithoutHaste/WithoutHaste.Drawing.Colors/blob/master/documentation/TableOfContents.WithoutHaste.Drawing.Colors.md)

[WithoutHaste.Drawing.Shapes](https://github.com/WithoutHaste/WithoutHaste.Drawing.Shapes/blob/master/documentation/TableOfContents.WithoutHaste.Drawing.Shapes.md)

[WithoutHaste.Windows.GUI](https://github.com/WithoutHaste/WithoutHaste.Windows.GUI/blob/master/documentation/TableOfContents.WithoutHaste.Windows.GUI.md)

The EarlyDocs test project includes examples of almost everything:
[EarlyDocs test project documentation](https://github.com/WithoutHaste/EarlyDocs/blob/master/Test/documentation/TableOfContents.Test.md)

## License

[MIT License](https://github.com/WithoutHaste/EarlyDocs/blob/master/LICENSE)

## Donate

[Become a patron](https://www.patreon.com/withouthaste) of this and other Without Haste open source projects.

## Version Notes

Uses [Semantic Versioning 2.0.0](https://semver.org/).

[Initial Release - v1.0.0](https://github.com/WithoutHaste/EarlyDocs/releases/tag/v1.0.0)
