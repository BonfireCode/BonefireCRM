// <copyright file="LogoutEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Security;
using BonefireCRM.API.Security.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class LogoutEndpoint : Endpoint<LogoutRequest, Results<Ok, UnauthorizedHttpResult>>
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

        public override async Task<Results<Ok, UnauthorizedHttpResult>> ExecuteAsync(LogoutRequest request, CancellationToken ct)
        {
            if (request is not null)
            {
                await _securityService.LogoutUser(ct);
                return TypedResults.Ok();
            }

            return TypedResults.Unauthorized();
        }
    }
}
