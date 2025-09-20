// <copyright file="Program.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

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
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseFastEndpoints();

app.MapDefaultEndpoints();

app.Run();
