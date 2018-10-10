# EarlyDocs

Converter from Visual Studio XML documentation to Markdown files.
* Created for C# library projects
* Generates one .md file per Type
* Generates one TableOfContents.md file that links to everything else
    * Displays the summary of each Type
* Save everything to a "documentation" directory

EarlyDocs pulls additional information from the .dll itself to provide more than the XML documentation contains.

Plans: turn this into a tool that will run when a project is built, and can be installed as a NuGet package.

This library is under active development. Report bugs and request features on Github, or to wohaste@gmail.com.

## Supported Xml Tags

Supports all standard Microsoft XML tags.

See examples: [How to Use XML Comments](HowToUseXmlComments.md)

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

Projects with EarlyDocs-generated documentation.

[WithoutHaste.Drawing.Colors](https://github.com/WithoutHaste/WithoutHaste.Drawing.Colors/blob/master/documentation/TableOfContents.md)

[WithoutHaste.Drawing.Shapes](https://github.com/WithoutHaste/WithoutHaste.Drawing.Shapes/blob/master/documentation/TableOfContents.md)

[WithoutHaste.Windows.GUI](https://github.com/WithoutHaste/WithoutHaste.Windows.GUI/blob/master/documentation/TableOfContents.md)

## License

[MIT License](https://github.com/WithoutHaste/EarlyDocs/blob/master/LICENSE)

## Support

[Become a patron](https://www.patreon.com/withouthaste) of this and other Without Haste open source projects.

## Versions

Version 1 in development.