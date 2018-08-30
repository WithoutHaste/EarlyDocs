# EarlyDocs

Converter from Visual Studio XML documentation to Markdown files.
* Created for C# library projects
* Generates one .md file per Type
* Generates one TableOfContents.md file that links to everything else
* Save everything to a "documentation" directory

EarlyDocs pulls additional information from the .dll itself to provide more than the XML documentation contains.

Plans: turn this into a tool that will run when a project is built, and can be installed as a NuGet package.

This library is under active development. Report bugs and request features on Github, or to wohaste@gmail.com.

## Supported Xml Tags

Top-level tags:  
* summary
* remarks
* example
* param
* returns
* exception

Formatting within other tags:  
* para
* see
* code
* list type="bullet"
    * listheader
	    * plain text
    * item
	    * plain text
        * term
        * term with description

## Custom Tags

### cref

When the `cref` attribute matches a Type in the current assembly, the text will be displayed as a link to the documentation for that Type.

To link to a Type in another project, use custom attribute `url`. If the `url` ends with a file extension, the `url` will be used as the entire link. If it does not, it will interpreted as an EarlyDocs documentation location.

Example:  
```
<see cref="MyType"/>
results in link
[MyType](documentation/MyType.md)
```

Example:  
```
<see cref="MyType" url="http://otherproject.com/file.html"/>
results in link
[MyType](http://otherproject.com/file.html)
```

Example:  
```
<see cref="MyType" url="http://otherproject.com/folder/"/>
results in link
[MyType](http://otherproject.com/folder/documentation/MyType.md)
```

## Examples

Other projects with EarlyDocs-generated documentation.

[WithoutHaste.Drawing.Colors](https://github.com/WithoutHaste/WithoutHaste.Drawing.Colors/blob/master/documentation/TableOfContents.md)

[WithoutHaste.Drawing.Shapes](https://github.com/WithoutHaste/WithoutHaste.Drawing.Shapes/blob/master/documentation/TableOfContents.md)

[WithoutHaste.Windows.GUI](https://github.com/WithoutHaste/WithoutHaste.Windows.GUI/blob/master/documentation/TableOfContents.md)

## Versions

Version 1 in development.