// <copyright file="DeleteContactEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Extensions;
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

            Summary(s =>
            {
                s.Summary = "Deletes an existing contact.";
                s.Description = "Removes a contact record identified by its unique ID. Returns 204 if deleted successfully, or 404 if the call was not found.";

                s.Params["id"] = "The unique identifier (GUID) of the contact to delete.";

                s.AddDeleteResponses("Contact");
            });
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
