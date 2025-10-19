// <copyright file="CreateInfoEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Security;
using BonefireCRM.API.Security.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class CreateInfoEndpoint : Endpoint<CreateInfoRequest, Results<Ok<CreateInfoResponse>, ProblemHttpResult>>
    {
        private readonly SecurityService _securityService;

        public CreateInfoEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Post("/security/info");
        }

        public override async Task<Results<Ok<CreateInfoResponse>, ProblemHttpResult>> ExecuteAsync(CreateInfoRequest request, CancellationToken ct)
        {
            var claimsPrincipal = User;

            var createInfoDTO = request.MapToDto();

            var result = await _securityService.ManageCreateUserInfo(createInfoDTO, claimsPrincipal, ct);

            var response = result.Match<Results<Ok<CreateInfoResponse>, ProblemHttpResult>>
            (
                dto => TypedResults.Ok(dto.MapToResponse()),
                error => TypedResults.Problem(error.Message, statusCode: StatusCodes.Status400BadRequest)
            );

            return response;
        }
    }
}
