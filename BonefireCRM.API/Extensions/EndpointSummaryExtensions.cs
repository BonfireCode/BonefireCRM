// <copyright file="EndpointSummaryExtensions.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Extensions
{
    public static class EndpointSummaryExtensions
    {
        public static void AddGetAllResponses<TResponse>(this EndpointSummary summary, string entityName, bool allowAnonymous = false)
        {
            summary.AddCommonResponses(entityName, "retrieving", allowAnonymous);
            summary.Response<Ok<TResponse>>(200, $"{entityName} details successfully retrieved.");
        }

        public static void AddCreateResponses<TResponse>(this EndpointSummary summary, string entityName, bool allowAnonymous = false)
        {
            summary.AddCommonResponses(entityName, "creating", allowAnonymous);
            summary.Response<Created<TResponse>>(201, $"{entityName} successfully created.");
        }

        public static void AddDeleteResponses(this EndpointSummary summary, string entityName, bool allowAnonymous = false)
        {
            summary.AddCommonResponses(entityName, "deleting", allowAnonymous);
            summary.Response<NoContent>(204, $"{entityName} successfully deleted.");
            summary.Response<NotFound>(404, $"The specified {entityName.ToLower()} could not be found.");
        }

        public static void AddGetResponses<TResponse>(this EndpointSummary summary, string entityName, bool allowAnonymous = false)
        {
            summary.AddCommonResponses(entityName, "retrieving", allowAnonymous);
            summary.Response<Ok<TResponse>>(200, $"{entityName} details successfully retrieved.");
            summary.Response<NotFound>(404, $"The specified {entityName.ToLower()} could not be found.");
        }

        public static void AddUpdateResponses<TResponse>(this EndpointSummary summary, string entityName, bool allowAnonymous = false)
        {
            summary.AddCommonResponses(entityName, "updating", allowAnonymous);
            summary.Response<Ok<TResponse>>(200, $"{entityName} successfully updated.");
            summary.Response<NotFound>(404, $"The specified {entityName.ToLower()} could not be found.");
        }

        private static void AddCommonResponses(this EndpointSummary summary, string entityName, string action, bool allowAnonymous)
        {
            var entity = entityName.ToLower();

            summary.Response<ProblemDetails>(400, "Invalid request data. Returns validation problem details.", "application/problem+json");
            summary.Response<InternalServerError>(500, $"An internal server error occurred while {action} the {entity}.");

            if (!allowAnonymous)
            {
                summary.Response<UnauthorizedHttpResult>(401, "User is not authorized to perform this action.");
            }
        }
    }
}
