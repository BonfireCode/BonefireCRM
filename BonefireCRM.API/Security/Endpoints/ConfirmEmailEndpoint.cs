// <copyright file="ConfirmEmailEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Security;
using BonefireCRM.API.Security.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class ConfirmEmailEndpoint : Endpoint<ConfirmEmailRequest, Results<Ok, UnauthorizedHttpResult>>
    {
        private readonly SecurityService _securityService;

        public ConfirmEmailEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Get("/security/confirmemail");
            AllowAnonymous();

            Summary(s =>
            {
                s.Summary = "Confirms a user's email address.";
                s.Description = "Verifies a user's email address using the provided user ID and confirmation token. " +
                                "If valid, the user's email address is confirmed. If the email is being changed, " +
                                "the new email address will replace the previous one upon confirmation.";

                s.Response<Ok>(200, "Email address successfully confirmed.");
                s.Response<UnauthorizedHttpResult>(401, "Invalid or expired confirmation token.");
                s.Response<ProblemHttpResult>(400, "Malformed request or missing parameters.");
                s.Response<InternalServerError>(500, "An internal server error occurred while confirming the email address.");
            });
        }

        public override async Task<Results<Ok, UnauthorizedHttpResult>> ExecuteAsync(ConfirmEmailRequest request, CancellationToken ct)
        {
            var confirmEmailDTO = request.MapToDto();

            var result = await _securityService.ConfirmUserEmail(confirmEmailDTO, ct);

            var response = result.Match<Results<Ok, UnauthorizedHttpResult>>
            (
                isEmailConfirmed => TypedResults.Ok(),
                _ => TypedResults.Unauthorized()
            );

            return response;
        }
    }
}
