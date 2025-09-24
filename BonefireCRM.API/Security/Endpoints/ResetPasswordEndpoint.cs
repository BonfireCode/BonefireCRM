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
            Post("/resetpassword");
            AllowAnonymous();
        }

        public override async Task<Results<Ok, ProblemHttpResult>> ExecuteAsync(ResetPasswordRequest request, CancellationToken ct)
        {
            var resetPasswordDTO = RequestToDtoMapper.MapToDto(request);

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
