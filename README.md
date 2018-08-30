# EarlyDocs

Converter from Visual Studio XML documentation to Markdown files.
* Created for C# library projects
* Generates one .md file per Type
* Generates one TableOfContents.md file that links to everything else
* Save everything to a "documentation" directory

EarlyDocs pulls additional information from the .dll itself to provide more than the XML documentation contains.

Plans: turn this into a tool that will run when a project is built, and can be installed as a NuGet package.

This library is under active development. Report bugs and request features on Github, or to wohaste@gmail.com.

## Examples

Other projects with EarlyDocs-generated documentation.

[WithoutHaste.Drawing.Colors](https://github.com/WithoutHaste/WithoutHaste.Drawing.Colors/documentation/TableOfContents.md)

[WithoutHaste.Drawing.Shapes](https://github.com/WithoutHaste/WithoutHaste.Drawing.Shapes/documentation/TableOfContents.md)

[WithoutHaste.Windows.GUI](https://github.com/WithoutHaste/WithoutHaste.Windows.GUI/documentation/TableOfContents.md)

## Versions

Version 1 in development.