var builder = DistributedApplication.CreateBuilder(args);

var apiService = builder.AddProject<Projects.BonefireCRM_API>("apiservice");

builder.AddProject<Projects.BonefireCRM_Web_Server>("webfrontend")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();
