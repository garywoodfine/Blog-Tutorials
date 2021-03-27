var target = Argument("target", "Default");
var configuration = Argument("configuration", "Release");

//////////////////////////////////////////////////////////////////////
// TASKS
//////////////////////////////////////////////////////////////////////

Task("Clean")
    .Does(() => {
    DotNetCoreClean("./");
});

Task("Build")
    .IsDependentOn("Clean")
    .Does(() => {
     var buildSettings = new DotNetCoreBuildSettings {
                        Configuration = configuration,
                       };
     var projects = GetFiles("./**/*.csproj");
     foreach(var project in projects )
     {
         Information($"Building {project.ToString()}");
         DotNetCoreBuild(project.ToString(),buildSettings);
     }
});

Task("Restore")
    .Description("Restoring the solution dependencies")
    .Does(() => {
           var projects = GetFiles("./**/*.csproj");

              foreach(var project in projects )
              {
                  Information($"Building { project.ToString()}");
                  DotNetCoreRestore(project.ToString());
              }

});

Task("Test")
    .IsDependentOn("Build")
    .Does(() => {

       var testSettings = new DotNetCoreTestSettings  {
                                  Configuration = configuration,
                                  NoBuild = true,
                              };
     var projects = GetFiles("./tests/*/*.csproj");
     foreach(var project in projects )
     {
       Information($"Running Tests : { project.ToString()}");
       DotNetCoreTest(project.ToString(), testSettings );
     }


});


//////////////////////////////////////////////////////////////////////
// EXECUTION
//////////////////////////////////////////////////////////////////////

Task("Default")
       .IsDependentOn("Clean")
       .IsDependentOn("Restore")
       .IsDependentOn("Build")
       .IsDependentOn("Test");

RunTarget(target);