// <copyright file="ForgotPasswordEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Security;
using BonefireCRM.API.Security.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class ForgotPasswordEndpoint : Endpoint<ForgotPasswordRequest, Ok>
    {
        private readonly SecurityService _securityService;

        public ForgotPasswordEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Post("/security/forgotpassword");
            AllowAnonymous();

            Summary(s =>
            {
                s.Summary = "Sends a password reset email.";
                s.Description = "Initiates the password reset process for the user associated with the provided email address. " +
                           "If a matching confirmed user exists, a password reset code is sent to their email.";

                s.Response<Ok>(200, "Password reset email sent successfully, if the provided email exists.");
                s.Response<ProblemDetails>(400, "Invalid request data. Returns validation problem details.", "application/problem+json");
                s.Response<InternalServerError>(500, "An internal server error occurred while processing the password reset request.");
            });
        }

        public override async Task<Ok> ExecuteAsync(ForgotPasswordRequest request, CancellationToken ct)
        {
            var forgotPasswordDTO = request.MapToDto();

            await _securityService.ForgotUserPassword(forgotPasswordDTO, ct);

            return TypedResults.Ok();
        }
    }
}
