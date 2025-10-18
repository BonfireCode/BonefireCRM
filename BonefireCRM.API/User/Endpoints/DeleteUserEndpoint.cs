// <copyright file="DeleteUserEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using System.Security.Claims;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.User.Endpoints
{
    public class DeleteUserEndpoint : EndpointWithoutRequest<Results<NoContent, NotFound>>
    {
        private readonly UserService _userService;
        private readonly ITransactionManager _transactionManager;

        public DeleteUserEndpoint(UserService UserService, ITransactionManager transactionManager)
        {
            _userService = UserService;
            _transactionManager = transactionManager;
        }

        public override void Configure()
        {
            Delete("/Users/{id:guid}");
            AllowAnonymous();
        }

        public override async Task<Results<NoContent, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var result = await _transactionManager.Execute(() => _userService.DeleteUserAsync(id, registerId, ct));
            var response = result.Match<Results<NoContent, NotFound>>
            (
                succ => TypedResults.NoContent(),
                _ => TypedResults.NotFound()
            );

            return response;
        }
    }
}
