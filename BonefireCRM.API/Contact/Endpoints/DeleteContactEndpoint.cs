// <copyright file="DeleteContactEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Contact.Endpoints
{
    public class DeleteContactEndpoint : EndpointWithoutRequest<Results<NoContent, NotFound>>
    {
        private readonly ContactService _contactService;

        public DeleteContactEndpoint(ContactService contactService)
        {
            _contactService = contactService;
        }

        public override void Configure()
        {
            Delete("/contacts/{id:guid}");
            AllowAnonymous();
        }

        public override async Task<Results<NoContent, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var isDeleted = await _contactService.DeleteContactAsync(id, ct);
            if (isDeleted)
            {
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }
    }
}
