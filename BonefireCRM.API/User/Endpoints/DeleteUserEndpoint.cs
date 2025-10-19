// <copyright file="DeleteUserEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace BonefireCRM.API.User.Endpoints
{
    public class DeleteUserEndpoint : EndpointWithoutRequest<Results<NoContent, NotFound>>
    {
        private readonly UserService _userService;

        public DeleteUserEndpoint(UserService UserService)
        {
            _userService = UserService;
        }

        public override void Configure()
        {
            Delete("/Users/{id:guid}");
            AllowAnonymous();

            Summary(s =>
            {
                s.Summary = "Deletes an existing user.";
                s.Description = "Removes a user record identified by its unique ID. Returns 204 if deleted successfully, or 404 if the call was not found.";
                s.Params["id"] = "The unique identifier (GUID) of the user to delete.";
                s.AddDeleteResponses("User", true);
            });
        }

        public override async Task<Results<NoContent, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _userService.DeleteUserAsync(id, registerId, ct);
            var response = result.Match<Results<NoContent, NotFound>>
            (
                Succ => TypedResults.NoContent(),
                _ => TypedResults.NotFound()
            );

            return response;
        }
    }
}
