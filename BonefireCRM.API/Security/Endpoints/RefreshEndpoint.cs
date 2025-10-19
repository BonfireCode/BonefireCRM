// <copyright file="RefreshEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Security;
using BonefireCRM.API.Security.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class RefreshEndpoint : Endpoint<RefreshRequest, Results<SignInHttpResult, ChallengeHttpResult>>
    {
        private readonly SecurityService _securityService;

        public RefreshEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Post("/security/refresh");

            Summary(s =>
            {
                s.Summary = "Refreshes the user's authentication token.";
                s.Description = "Uses a valid refresh token to generate a new access token and extend the user's authenticated session. " +
                                "If the provided refresh token is invalid, expired, or revoked, the client is challenged to re-authenticate.";

                s.Response<SignInHttpResult>(200, "A new access token was successfully issued.");
                s.Response<ChallengeHttpResult>(401, "The provided refresh token is invalid, expired, or revoked. The client must log in again.");
                s.Response<InternalServerError>(500, "An internal server error occurred while refreshing the token.");
            });
        }

        public override async Task<Results<SignInHttpResult, ChallengeHttpResult>> ExecuteAsync(RefreshRequest request, CancellationToken ct)
        {
            var refreshDTO = request.MapToDto();

            var result = await _securityService.RefreshUserToken(refreshDTO, ct);

            if (!result.IsTokenRefreshed)
            {
                return TypedResults.Challenge();
            }

            return TypedResults.SignIn(result.ClaimsPrincipal, authenticationScheme: result.AuthenticationScheme);
        }
    }
}