// <copyright file="Program.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API;
using FastEndpoints;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseKestrel(options => options.AddServerHeader = false);

builder.Services.AddFastEndpoints();

builder.AddDomainDependencies();
builder.AddInfrastructureDependencies();

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

// Add services to the container.
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// Learn more about configuring OpenAPI for scalar authentication at https://github.com/scalar/scalar/blob/main/integrations/aspnetcore/docs/authentication.md
builder.Services.AddOpenApiWithBearerAuth();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MigrateDatabases();
    app.MapOpenApi();

    app.MapScalarApiReference(options =>
        options.WithPersistentAuthentication());
}

app.UseAuthentication();
app.UseAuthorization();

app.UseFastEndpoints(c =>
{
    c.Endpoints.RoutePrefix = "api";
});

app.MapDefaultEndpoints();

app.Run();
