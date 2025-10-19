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
            Post("/security/login");
            AllowAnonymous();

            Summary(s =>
            {
                s.Summary = "Authenticates a user and starts a new session.";
                s.Description = "Validates the provided username and password. " +
                                "If successful, returns authentication cookies or a bearer token depending on the request parameters. " +
                                "Supports optional two-factor authentication if enabled.";

                s.Response<EmptyHttpResult>(200, "User successfully authenticated. Returns authentication cookies or bearer token.");
                s.Response<ProblemHttpResult>(400, "Invalid request data or missing parameters.");
                s.Response<ProblemHttpResult>(401, "Invalid username, password, or two-factor credentials.");
                s.Response<InternalServerError>(500, "An internal server error occurred while processing the login request.");
            });
        }

        public override async Task<Results<EmptyHttpResult, ProblemHttpResult>> ExecuteAsync(LoginRequest request, CancellationToken ct)
        {
            var useCookies = Query<bool>("useCookies", isRequired: false);
            var useSessionCookies = Query<bool>("useSessionCookies", isRequired: false);

            var loginDTO = request.MapToDto(useCookies, useSessionCookies);

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