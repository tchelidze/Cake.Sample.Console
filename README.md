**Cake.Sample**

Contains [Cake](https://cakebuild.net/) build script with following tasks

**BuildSrc** - Builds `Cake.Sample.Console.csproj` project and puts output into `artifacts\build\src` folder.

**BuildTest** - Builds `Cake.Sample.Tests.csproj` project and puts output into `artifacts\build\test` folder.

**Pack** - Generates Nuget package (`.nupkg` file) and puts it into `artifacts\nuget` folder.

**Test** - Runs all the unit tests inside `Cake.Sample.Tests.csproj` and outputs result in console.

**CleanAll** - Cleans up `artifacts` folder.


**Q**: How to run ? 

**A**: Open up the powershell and navigate to root directory (where `build.ps1` file is) and run ` ./build.ps1 -target [TaskName]`, ex, ` ./build.ps1 -target Test`


**Q**: Any bonus ?

**A**: Yup, you can find sample of `MSpec` unit tests (so exciting..)
