// <copyright file="ResendConfirmationEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Security;
using BonefireCRM.API.Security.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class ResendConfirmationEndpoint : Endpoint<ResendConfirmationRequest, Ok>
    {
        private readonly SecurityService _securityService;

        public ResendConfirmationEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Post("/security/resendconfirmation");
            AllowAnonymous();

            Summary(s =>
            {
                s.Summary = "Resends the account confirmation email.";
                s.Description = "Sends a new confirmation email to the provided address if the user exists and has not yet confirmed their email.";

                s.Response<Ok>(200, "Confirmation email successfully resent, if applicable.");
                s.Response<ProblemHttpResult>(400, "Invalid request data or the email is already confirmed.");
                s.Response<InternalServerError>(500, "An internal server error occurred while resending the confirmation email.");
            });
        }

        public override async Task<Ok> ExecuteAsync(ResendConfirmationRequest request, CancellationToken ct)
        {
            var resendConfirmationDTO = request.MapToDto();

            await _securityService.ResendUserConfirmation(resendConfirmationDTO, ct);

            return TypedResults.Ok();
        }
    }
}
