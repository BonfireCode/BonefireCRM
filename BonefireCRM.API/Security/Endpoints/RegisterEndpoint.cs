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
            Post("/security/register");
            AllowAnonymous();

            Summary(s =>
            {
                s.Summary = "Registers a new user account.";
                s.Description = "Creates a new user with the provided username, email, and password. " +
                                "After successful registration, an email confirmation may be required before login.";

                s.Response<Ok<Guid>>(200, "User registered successfully. Returns the unique user ID.");
                s.Response<ProblemHttpResult>(400, "Invalid registration data or user already exists.");
                s.Response<InternalServerError>(500, "An internal server error occurred while registering the user.");
            });
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