// <copyright file="LoginEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Security;
using BonefireCRM.API.Security.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class LoginEndpoint : Endpoint<LoginRequest, Results<EmptyHttpResult, ProblemHttpResult>>
    {
        private readonly SecurityService _securityService;

        public LoginEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Post("/login");
            AllowAnonymous();
        }

        public override async Task<Results<EmptyHttpResult, ProblemHttpResult>> ExecuteAsync(LoginRequest request, CancellationToken ct)
        {
            var useCookies = Query<bool>("useCookies", isRequired: false);
            var useSessionCookies = Query<bool>("useSessionCookies", isRequired: false);

            var loginDTO = RequestToDtoMapper.MapToDto(request, useCookies, useSessionCookies);

            var result = await _securityService.LoginUser(loginDTO, ct);

            if (!result.Succeeded)
            {
                return TypedResults.Problem(result.ToString(), statusCode: StatusCodes.Status401Unauthorized);
            }

            // The signInManager already produced the needed response in the form of a cookie or bearer token.
            return TypedResults.Empty;
        }
    }
}