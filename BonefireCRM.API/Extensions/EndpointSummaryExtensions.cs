// <copyright file="EndpointSummaryExtensions.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using System.ComponentModel;
using System.Reflection;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Extensions
{
    public static class EndpointSummaryExtensions
    {
        public static void AddParamsFrom<TRequest>(this EndpointSummary s)
        {
            var props = typeof(TRequest)
                .GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in props)
            {
                var description = prop.GetCustomAttribute<DescriptionAttribute>()?.Description ?? string.Empty;
                s.Params[prop.Name] = description;
            }
        }

        public static void AddCreateResponses<TResponse>(this EndpointSummary s, string entityName)
        {
            s.Response<Created<TResponse>>(201, $"{entityName} successfully created.");
            s.Response<ProblemDetails>(400, "Invalid request data. Returns validation problem details.", "application/problem+json");
            s.Response<UnauthorizedHttpResult>(401, "User is not authorized to perform this action.");
            s.Response<InternalServerError>(500, $"An internal server error occurred while creating the {entityName.ToLower()}.");
        }

        public static void AddDeleteResponses(this EndpointSummary s, string entityName)
        {
            s.Response<NoContent>(204, $"{entityName} successfully deleted.");
            s.Response<NotFound>(404, $"The specified {entityName.ToLower()} could not be found.");
            s.Response<ProblemDetails>(400, "Invalid request data. Returns validation problem details.", "application/problem+json");
            s.Response<UnauthorizedHttpResult>(401, "User is not authorized to perform this action.");
            s.Response<InternalServerError>(500, $"An internal server error occurred while deleting the {entityName.ToLower()}.");
        }

        public static void AddGetResponses<TResponse>(this EndpointSummary s, string entityName)
        {
            s.Response<Ok<TResponse>>(200, $"{entityName} details successfully retrieved.");
            s.Response<NotFound>(404, $"The specified {entityName.ToLower()} could not be found.");
            s.Response<ProblemDetails>(400, "Invalid request data. Returns validation problem details.", "application/problem+json");
            s.Response<UnauthorizedHttpResult>(401, "User is not authorized to access this resource.");
            s.Response<InternalServerError>(500, $"An internal server error occurred while retrieving the {entityName.ToLower()} details.");
        }

        public static void AddUpdateResponses<TResponse>(this EndpointSummary s, string entityName)
        {
            s.Response<Ok<TResponse>>(200, $"{entityName} successfully updated.");
            s.Response<NotFound>(404, $"The specified {entityName.ToLower()} could not be found.");
            s.Response<ProblemDetails>(400, "Invalid request data. Returns validation problem details.", "application/problem+json");
            s.Response<UnauthorizedHttpResult>(401, "User is not authorized to perform this action.");
            s.Response<InternalServerError>(500, $"An internal server error occurred while updating the {entityName.ToLower()}.");
        }
    }
}
