// <copyright file="GetUserEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.User;
using BonefireCRM.API.Extensions;
using BonefireCRM.API.User.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.User.Endpoints
{
    public class GetUserEndpoint : EndpointWithoutRequest<Results<Ok<GetUserResponse>, NotFound>>
    {
        private readonly UserService _userService;

        public GetUserEndpoint(UserService userService)
        {
            _userService = userService;
        }

        public override void Configure()
        {
            Get("/users/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Retrieves a specific user by ID.";
                s.Description = "Fetches detailed information about a user identified by its unique ID.";
                s.Params["id"] = "The unique identifier (GUID) of the user to retrieve.";
                s.AddGetResponses<GetUserResponse>("User", true);
            });
        }

        public override async Task<Results<Ok<GetUserResponse>, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var result = await _userService.GetUserAsync(id, ct);

            var response = result.Match<Results<Ok<GetUserResponse>, NotFound>>(
                dtoUser => TypedResults.Ok(dtoUser.MapToResponse()),
                TypedResults.NotFound());

            return response;
        }
    }
}
