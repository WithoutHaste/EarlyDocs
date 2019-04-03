# EarlyDocs

EarlyDocs generates Markdown documentation for your .Net library.

Install the EarlyDocs NuGet package to insert EarlyDocs into your build process. It will run after your project build is complete. Errors in EarlyDocs will not affect your build.

EarlyDocs is built on top of WithoutHaste.DataFiles.  
[On NuGet](https://www.nuget.org/packages/WithoutHaste.DataFiles/)  
[On GitHub](https://github.com/WithoutHaste/WithoutHaste.DataFiles)  

This library is under active development. Report bugs and request features on Github, or to wohaste@gmail.com.

## Download

[Available on Nuget](https://www.nuget.org/packages/EarlyDocs).

Package Manager: `Install-Package EarlyDocs -Version 2.0.0`

.NET CLI: `dotnet add package EarlyDocs --version 2.0.0`

## Documentation

To get started with EarlyDocs:  
[Installing and Using EarlyDocs](USING_EARLYDOCS.md)  

About using Xml Comments:  
[How to Use Xml Comments](HowToUseXmlComments.md)  

For programmers on this project:  
[Design](DESIGN.md)  

## Examples

Projects with EarlyDocs-generated documentation.

[WithoutHaste.DataFiles](https://github.com/WithoutHaste/WithoutHaste.DataFiles/blob/master/documentation/TableOfContents.md)

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

[v2.0.0](https://github.com/WithoutHaste/EarlyDocs/releases/tag/v2.0.0)  
+ Fixed path error that caused process to fail.  
+ Project is not multi-targeted for all .Net frameworks from 2.0 through 4.7.2.

[Initial Release - v1.0.0](https://github.com/WithoutHaste/EarlyDocs/releases/tag/v1.0.0)
