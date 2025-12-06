var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.BonefireCRM_API>("apiservice");

//var webFrontend = builder.AddProject<Projects.BonefireCRM_Web_Server>("webfrontend")
//    .WithExternalHttpEndpoints()
//    .WithReference(apiService)
//    .WaitFor(apiService);

var frontend = builder.AddJavaScriptApp("frontend", "../BonefireCRM.Web/bonefirecrm.web.client", runScriptName: "start")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WithHttpEndpoint(env: "PORT")
    .WaitFor(apiService);

builder.Build().Run();
