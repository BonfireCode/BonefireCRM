// <copyright file="ResetPasswordEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Security;
using BonefireCRM.API.Security.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class ResetPasswordEndpoint : Endpoint<ResetPasswordRequest, Results<Ok, ProblemHttpResult>>
    {
        private readonly SecurityService _securityService;

        public ResetPasswordEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Post("/security/resetpassword");
            AllowAnonymous();

            Summary(s =>
            {
                s.Summary = "Resets the user's password using a valid reset code.";
                s.Description = "Resets the password for a user who has requested a password reset via the '/forgotpassword' endpoint. " +
                                "The reset code sent to the user's email must be valid and not expired.";

                s.Response<Ok>(200, "Password successfully reset.");
                s.Response<ProblemHttpResult>(400, "Invalid or expired reset code, or the provided data is invalid.");
                s.Response<InternalServerError>(500, "An internal server error occurred while resetting the password.");
            });
        }

        public override async Task<Results<Ok, ProblemHttpResult>> ExecuteAsync(ResetPasswordRequest request, CancellationToken ct)
        {
            var resetPasswordDTO = request.MapToDto();

            var result = await _securityService.ResetUserPassword(resetPasswordDTO, ct);

            var response = result.Match<Results<Ok, ProblemHttpResult>>
            (
                isPasswordReset => TypedResults.Ok(),
                _ => TypedResults.Problem("Invalid Token", statusCode: StatusCodes.Status400BadRequest)
            );

            return response;
        }
    }
}
