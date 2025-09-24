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
            Post("/forgotpassword");
            AllowAnonymous();
        }

        public override async Task<Ok> ExecuteAsync(ForgotPasswordRequest request, CancellationToken ct)
        {
            var forgotPasswordDTO = RequestToDtoMapper.MapToDto(request);

            await _securityService.ForgotUserPassword(forgotPasswordDTO, ct);

            return TypedResults.Ok();
        }
    }
}
