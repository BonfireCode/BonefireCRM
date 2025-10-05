// <copyright file="UpdateUserEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.User;
using BonefireCRM.API.User.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace BonefireCRM.API.User.Endpoints
{
    public class UpdateUserEndpoint : Endpoint<UpdateUserRequest, Results<Ok<UpdateUserResponse>, NotFound, InternalServerError>>
    {
        private readonly UserService _userService;

        public UpdateUserEndpoint(UserService UserService)
        {
            _userService = UserService;
        }

        public override void Configure()
        {
            Put("/Users/{id:guid}");
            AllowAnonymous();
        }

        public override async Task<Results<Ok<UpdateUserResponse>, NotFound, InternalServerError>> ExecuteAsync(UpdateUserRequest request, CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var dtoUser = request.MapToDto(id, registerId);

            var result = await _userService.UpdateUserAsync(dtoUser, ct);

            var response = result.Match<Results<Ok<UpdateUserResponse>, NotFound, InternalServerError>>(
                updatedUser => TypedResults.Ok(updatedUser.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
