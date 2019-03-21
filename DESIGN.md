[Home](README.md)

# Design

## EarlyDocs Solution

### EarlyDocs Project

EarlyDocs is built on top the [WithoutHaste.DataFiles](https://github.com/WithoutHaste/WithoutHaste.DataFiles).

The DataFiles library is published as it's own NuGet package. _(pending)_

EarlyDocs depends on the WithoutHaste.DataFiles Nuget package.

### EarlyDocsTest Project

EarlyDocsTest is unit tests for just the EarlyDocs project - not for any functionality that could be tested within DataFiles. It's barely used. Most debugging at this level is done by looking at the generated Markdown files in the Test project.

### Test Project

The Test project depends on a local test build of the EarlyDocs NuGet package. It generates lots of Markdown files for testing various .Net coding constructs.

## AutomatedOracleTests Solutions

_(in progress)_  

Everything in the AutomatedOracleTests folder is auto-generated based on the Test project.

AutomatedOracleTests are separate Solutions (so their Packages remain separate) that depend on each possible target framework of the EarlyDocs NuGet package.

The AutomatedOracleTests are generated and built automatically, which causes them to generate their Markdown documentation files. These files are automatically compared to the /Test/oracleDocumenation files. Any discrepancies are reported as errors.

