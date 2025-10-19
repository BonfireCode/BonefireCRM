// <copyright file="GenerateTwoFactorCodeEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    // THIS ENDPOINT IS ONLY FOR TESTING PURPOSES
    public class GenerateTwoFactorCodeEndpoint : EndpointWithoutRequest<Ok<string>>
    {
        private readonly SecurityService _securityService;

        public GenerateTwoFactorCodeEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Get("/security/generatecode2fa");
        }

        public override async Task<Ok<string>> ExecuteAsync(CancellationToken ct)
        {
            var claimsPrincipal = User;

            var result = await _securityService.GenerateTwoFactorCode(claimsPrincipal, ct);

            return TypedResults.Ok(result);
        }
    }
}
