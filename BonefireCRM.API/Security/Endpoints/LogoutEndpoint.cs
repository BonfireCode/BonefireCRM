// <copyright file="LogoutEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class LogoutEndpoint : Endpoint<EmptyRequest, Results<Ok, UnauthorizedHttpResult>>
    {
        private readonly SecurityService _securityService;

        public LogoutEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Post("/logout");
        }

        public override async Task<Results<Ok, UnauthorizedHttpResult>> ExecuteAsync(EmptyRequest request, CancellationToken ct)
        {
            var authHeader = HttpContext.Request.Headers["Authorization"].ToString();
            if (!string.IsNullOrEmpty(authHeader))
            {
                await _securityService.LogoutUser(ct);
                return TypedResults.Ok();
            }

            return TypedResults.Unauthorized();
        }
    }
}
