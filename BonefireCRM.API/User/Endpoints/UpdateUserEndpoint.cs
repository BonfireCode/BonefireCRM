// <copyright file="UpdateUserEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using System.Security.Claims;
using BonefireCRM.API.Contrat.User;
using BonefireCRM.API.Extensions;
using BonefireCRM.API.User.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

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
            Put("/users/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Updates an existing user.";
                s.Description = "Updates the details of an existing user identified by its unique ID.";
                s.Params["id"] = "The unique identifier (GUID) of the user to update.";
                s.AddUpdateResponses<UpdateUserResponse>("User", true);
            });
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
