// <copyright file="Program.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API;
using BonefireCRM.API.Exception;
using BonefireCRM.API.Telemetry;
using FastEndpoints;
using FastEndpoints.Swagger;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.AddDomainDependencies();
builder.AddInfrastructureDependencies();

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.Services.AddProblemDetails(options =>
{
    options.CustomizeProblemDetails = ctx =>
    {
        ctx.ProblemDetails.Instance = $"{ctx.HttpContext.Request.Method} {ctx.HttpContext.Request.Path}";
        ctx.ProblemDetails.Extensions.TryAdd("requestId", ctx.HttpContext.TraceIdentifier);
    };
});
builder.Services.AddExceptionHandler<GlobalExceptionHandler>();

builder.Services.AddFastEndpoints()
    .SwaggerDocument(o =>
    {
        o.DocumentSettings = s =>
        {
            s.Title = "Bonefire-CRM";
            s.Version = "v1";
        };
    });

var app = builder.Build();

app.UseExceptionHandler();

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
    c.Endpoints.Configurator = ep =>
    {
        ep.Options(b => b.AddEndpointFilter<GlobalLoggingFilter>());
    };
});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.MigrateDatabases();

app.UseOpenApi(c => c.Path = "/openapi/{documentName}.json");
app.MapScalarApiReference(options =>
    options.EnablePersistentAuthentication());

app.MapDefaultEndpoints();

app.MapTestingFrontendEndpoints();

app.MapFallbackToFile("/index.html");

app.Run();
