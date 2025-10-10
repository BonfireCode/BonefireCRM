// <copyright file="GetUserEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.User.Mappers;
using BonefireCRM.API.Contrat.User;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.User.Endpoints
{
    public class GetUserEndpoint : EndpointWithoutRequest<Results<Ok<GetUserResponse>, NotFound>>
    {
        private readonly UserService _userService;

        public GetUserEndpoint(UserService UserService)
        {
            _userService = UserService;
        }

        public override void Configure()
        {
            Get("/Users/{id:guid}");
            AllowAnonymous();
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
