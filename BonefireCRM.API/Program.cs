// <copyright file="Program.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using FastEndpoints;
using FastEndpoints.Swagger;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddFastEndpoints()
    .SwaggerDocument(o =>
    {
        o.DocumentSettings = s =>
        {
            s.Title = "Bonefire-CRM";
            s.Version = "v1";
        };
    });

builder.AddDomainDependencies();
builder.AddInfrastructureDependencies();

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

var app = builder.Build();

app.UseExceptionHandler();

app.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
});

if (app.Environment.IsDevelopment())
{
    app.MigrateDatabases();
    app.UseOpenApi(option =>
    option.Path = "/openapi/{documentName}.json");

    app.MapScalarApiReference(options =>
        options.WithPersistentAuthentication());
}

app.UseAuthentication();
app.UseAuthorization();

app.MapDefaultEndpoints();

app.Run();
