#tool nuget:?package=GitVersion.CommandLine&version=4.0.0
#tool "nuget:?package=Machine.Specifications.Runner.Console"

//////////////////////////////////////////////////////////////////////
// ARGUMENTS
//////////////////////////////////////////////////////////////////////
var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// GLOBAL VARIABLES
//////////////////////////////////////////////////////////////////////

var artifactsFolder = MakeAbsolute(Directory("./artifacts"));

var nugetsFolder = MakeAbsolute(Directory($"{artifactsFolder}/nuget"));
var buildFolder = MakeAbsolute(Directory($"{artifactsFolder}/build"));
var srcBuildFolder = MakeAbsolute(Directory($"{buildFolder}/src"));
var testBuildFolder = MakeAbsolute(Directory($"{buildFolder}/test")); 

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Default")
  .IsDependentOn("BuildSrc");

Task("CleanBase")
  .Does(() => 
  {
    CleanDirectories("./src/**/bin");
    CleanDirectories("./src/**/obj");
    CleanDirectories("./test/**/bin");
    CleanDirectories("./test/**/obj");
  });


Task("CleanSrc")
.IsDependentOn("CleanBase")
  .Does(() => 
  {
      CleanDirectory(srcBuildFolder);
  });

Task("CleanTest")
  .IsDependentOn("CleanBase")
    .Does(() => 
    {
        CleanDirectory(testBuildFolder); 
    });

Task("CleanPack")
  .IsDependentOn("CleanBase")
      .Does(() => 
      {
          CleanDirectory(nugetsFolder); 
      }); 

Task("CleanAll")
  .IsDependentOn("CleanSrc")
  .IsDependentOn("CleanTest")
  .IsDependentOn("CleanPack");

Task("BuildSrc")
  .IsDependentOn("CleanSrc")
  .Does(() => {
    MSBuild("./src/Cake.Sample.Console/Cake.Sample.Console.csproj", new MSBuildSettings()
      .SetConfiguration(configuration)
      .WithProperty("OutDir",  srcBuildFolder.ToString())
      .SetVerbosity(Verbosity.Diagnostic));
  });

Task("BuildPack")
  .IsDependentOn("CleanPack")
  .Does(() => {
    MSBuild("./src/Cake.Sample.Console/Cake.Sample.Console.csproj", new MSBuildSettings()
      .SetConfiguration(configuration)
      .SetVerbosity(Verbosity.Diagnostic));
  });

Task("BuildTest")
  .IsDependentOn("CleanTest")
  .Does(() => {
    MSBuild("./test/Cake.Sample.Tests/Cake.Sample.Tests.csproj", new MSBuildSettings()
      .WithProperty("OutDir",  testBuildFolder.ToString())
      .SetVerbosity(Verbosity.Diagnostic));
  });

Task("Pack")
    .IsDependentOn("BuildPack")
    .Does(() =>{

      NuGetPack($"src/Cake.Sample.Console/Cake.Sample.Console.csproj", new NuGetPackSettings{
          Id = "Cake.Sample",
          Verbosity = NuGetVerbosity.Detailed,
          OutputDirectory = nugetsFolder, 
          Properties = new Dictionary<string, string>
          {
            { "Configuration", configuration }
          },
          Description = "Sample Cheesy Cake", 
          Authors = new []{ "Bitchiko Tchelidze" }, 
      }
    );
});

 Task("Test")
    .IsDependentOn("BuildTest")
    .Does(() => {
        MSpec($"{testBuildFolder}/*.Tests.dll", new MSpecSettings());
      });

 RunTarget(target);