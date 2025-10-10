// <copyright file="RegisterEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Security;
using BonefireCRM.API.Security.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Security.Endpoints
{
    public class RegisterEndpoint : Endpoint<RegisterRequest, Results<Ok<Guid>, ProblemHttpResult>>
    {
        private readonly SecurityService _securityService;

        public RegisterEndpoint(SecurityService securityService)
        {
            _securityService = securityService;
        }

        public override void Configure()
        {
            Post("/register");
            AllowAnonymous();
        }

        public override async Task<Results<Ok<Guid>, ProblemHttpResult>> ExecuteAsync(RegisterRequest request, CancellationToken ct)
        {
            var registerDTO = request.MapToDto();

            var result = await _securityService.RegisterUser(registerDTO, ct);

            var response = result.Match<Results<Ok<Guid>, ProblemHttpResult>>
            (
                dto => TypedResults.Ok(dto.UserId),
                error => TypedResults.Problem(error.Message, statusCode: StatusCodes.Status400BadRequest)
            );

            return response;
        }
    }
}