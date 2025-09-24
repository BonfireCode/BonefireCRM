// <copyright file="Extensions.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

namespace BonefireCRM.API
{
    /// <summary>
    /// Provides extension methods for configuring the API, such as OpenAPI.
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Adds OpenAPI with a global JWT Bearer authentication scheme.
        /// Makes the Bearer "Authorize" field appear in Scalar UI.
        /// </summary>
        /// /// <param name="services">The service collection to add OpenAPI to.</param>
        /// <returns>The updated <see cref="IServiceCollection"/>.</returns>
        public static IServiceCollection AddOpenApiWithBearerAuth(this IServiceCollection services)
        {
            services.AddOpenApi(options =>
            {
                options.AddDocumentTransformer((document, _, _) =>
                {
                    var securityScheme = new OpenApiSecurityScheme
                    {
                        Type = SecuritySchemeType.Http,
                        In = ParameterLocation.Header,
                        Scheme = "bearer",
                    };

                    document.Components ??= new OpenApiComponents();
                    document.Components.SecuritySchemes.Add(JwtBearerDefaults.AuthenticationScheme, securityScheme);

                    var referenceScheme = new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Id = JwtBearerDefaults.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme,
                        },
                    };

                    document.SecurityRequirements.Add(new OpenApiSecurityRequirement
                    {
                        [referenceScheme] = [],
                    });

                    return Task.CompletedTask;
                });
            });

            return services;
        }
    }
}
