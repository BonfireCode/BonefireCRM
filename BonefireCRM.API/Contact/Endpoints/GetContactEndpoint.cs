// <copyright file="GetContactEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contact.Mappers;
using BonefireCRM.API.Contrat.Contact;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Contact.Endpoints
{
    public class GetContactEndpoint : EndpointWithoutRequest<Results<Ok<GetContactResponse>, NotFound>>
    {
        private readonly ContactService _contactService;

        public GetContactEndpoint(ContactService contactService)
        {
            _contactService = contactService;
        }

        public override void Configure()
        {
            Get("/contacts/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Retrieves a specific contact by ID.";
                s.Description = "Fetches detailed information about a contact identified by its unique ID.";

                s.Params["id"] = "The unique identifier (GUID) of the contact to retrieve.";

                s.AddGetResponses<GetContactResponse>("Contact");
            });
        }

        public override async Task<Results<Ok<GetContactResponse>, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var result = await _contactService.GetContactAsync(id, ct);

            var response = result.Match<Results<Ok<GetContactResponse>, NotFound>>(
                dtoContact => TypedResults.Ok(dtoContact.MapToResponse()),
                TypedResults.NotFound());

            return response;
        }
    }
}
