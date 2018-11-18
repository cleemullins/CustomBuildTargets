# Summary
Experimenting with MSBuild, .Net Core, and how custom tasks can participate in the build lifecyle. 

# Description
This project defines a simple task that participates in the .Net Core build lifecycle. The goal is to have code in a project that examines the project configuration and takes action as necessary. By having the code in the project, external dependencies and complexities are avoided and the build is easily manipulated. 

This code will (eventually) run as part of the Publish pipeline (hooked on the PrePublish event) and validate the .Bot file and block publishing if the project isn't configured as needed for the code.
``` 
BeforeTargets="BeforePublish"
```
The value of this is to "Fail Fast" during the publish process, as it's better to get an accurate error message (and a failed publish) rather than having publish to Azure succeed and then having to debug internal server errors. 

The Task is registered with the build system via:
```
 <UsingTask TaskName="PublishValidationTask" AssemblyFile="bin\debug\netcoreapp2.0\PublishPipelineTesting.dll" />
```

 The task is defined in the .csproj file
```
 <Target Name="MyTarget" AfterTargets="TestTarget">
    <PublishValidationTask />
 </Target>
```

Depending on how the task is run (return true / return false) the build will succeeed or fail. By having the task return "false", the build won't pass and the project cannot be run. 
```
C:\git\CustomBuildTargets\PublishPipelineTesting>dotnet run PublishPipelineTesting.csproj

The build failed. Please fix the build errors and run again.
```

Returning "True" out of the build task allows the project to build and run:
```
C:\git\CustomBuildTargets\PublishPipelineTesting>dotnet run PublishPipelineTesting.csproj
Hello World!
```

To observe the build chain, a dotnet msbuild command can be run:
```
C:\git\CustomBuildTargets\PublishPipelineTesting>dotnet msbuild PublishPipelineTesting.csproj
Microsoft (R) Build Engine version 15.7.179.6572 for .NET Core
Copyright (C) Microsoft Corporation. All rights reserved.

  PublishPipelineTesting -> C:\git\CustomBuildTargets\PublishPipelineTesting\bin\Debug\netcoreapp2.0\PublishPipelineTesting.dll
  This is a test
  Hello World
```