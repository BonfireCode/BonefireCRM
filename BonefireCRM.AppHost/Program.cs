var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.BonefireCRM_API>("apiservice");

var frontend = builder.AddJavaScriptApp("frontend", "../BonefireCRM.Web/bonefirecrm.web.client", runScriptName: "start")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WithHttpEndpoint(env: "PORT")
    .WaitFor(apiService);

builder.Build().Run();
