// <copyright file="TwoFactorEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Security;
using BonefireCRM.API.Security.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class TwoFactorEndpoint : Endpoint<TwoFactorRequest, Results<Ok<TwoFactorResponse>, ProblemHttpResult>>
    {
        private readonly SecurityService _securityService;

        public TwoFactorEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Post("/security/2fa");

            Summary(s =>
            {
                s.Summary = "Manages two-factor authentication (2FA) settings for the authenticated user.";
                s.Description = "Allows the authenticated user to enable, disable, or reset two-factor authentication (2FA). " +
                                "It can also regenerate shared keys and recovery codes if requested.";

                s.Response<Ok<TwoFactorResponse>>(200, "Two-factor authentication settings successfully updated.");
                s.Response<ProblemHttpResult>(400, "Invalid request data or two-factor verification failed.");
                s.Response<UnauthorizedHttpResult>(401, "User is not authorized to modify two-factor settings.");
                s.Response<InternalServerError>(500, "An internal server error occurred while updating two-factor settings.");
            });
        }

        public override async Task<Results<Ok<TwoFactorResponse>, ProblemHttpResult>> ExecuteAsync(TwoFactorRequest request, CancellationToken ct)
        {
            var claimsPrincipal = User;

            var twoFactorDTO = request.MapToDto();

            var result = await _securityService.ManageUserTwoFactor(twoFactorDTO, claimsPrincipal, ct);

            var response = result.Match<Results<Ok<TwoFactorResponse>, ProblemHttpResult>>
            (
                dto => TypedResults.Ok(dto.MapToResponse()),
                error => TypedResults.Problem(error.Message)
            );

            return response;
        }
    }
}
