# EarlyDocs

XML to MD documentation converter for Visual Studio XML documentation.

Instead of making a single .md file, it will generate a TableOfContents file linked to one file per Type, and all the files will be saved in a "documentation" folder.

Also pulls additional information from the dll itself to provide more than the XML documentation provides.

The goal is to make this into a tool that will run when a project is built, and can be installed as a NuGet package.

This library is under active development. Report bugs and request features on Github, or to wohaste@gmail.com.

## Custom Tags

### constant

Add `<constant/>` tag to comments before a Field to help organize documentation.

```
/// <summary>Description</summary>
/// <constant/>
public const int MyFieldA...
/// <summary>Description</summary>
/// <constant/>
public readonly int MyFieldB...
```

## Examples

Other projects with EarlyDocs-generated documentation.

[WithoutHaste.Drawing.Colors](https://github.com/WithoutHaste/WithoutHaste.Drawing.Colors/documentation/TableOfContents.md)

[WithoutHaste.Drawing.Shapes](https://github.com/WithoutHaste/WithoutHaste.Drawing.Shapes/documentation/TableOfContents.md)

[WithoutHaste.Windows.GUI](https://github.com/WithoutHaste/WithoutHaste.Windows.GUI/documentation/TableOfContents.md)

## Versions

Version 1 in development.