**Cake.Sample**

Contains [Cake](https://cakebuild.net/) build script containing following tasks

**BuildSrc** - Builds `Cake.Sample.Console.csproj` project and puts output into `artifacts\build\src` folder.

**BuildTest** - Builds `Cake.Sample.Tests.csproj` project and puts output into `artifacts\build\test` folder.

**Pack** - Generates Nuget package (`.nupkg` file) and puts it into `artifacts\nuget` folder.

**Test** - Runs all the unit tests inside `Cake.Sample.Tests.csproj` and outputs result in console.

**CleanAll** - Cleans up `artifacts` folder.
