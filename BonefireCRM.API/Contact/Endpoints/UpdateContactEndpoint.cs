// <copyright file="UpdateContactEndpoint.cs" company="Bonefire">
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
    public class UpdateContactEndpoint : Endpoint<UpdateContactRequest, Results<Ok<UpdateContactResponse>, NotFound, InternalServerError>>
    {
        private readonly ContactService _contactService;

        public UpdateContactEndpoint(ContactService contactService)
        {
            _contactService = contactService;
        }

        public override void Configure()
        {
            Put("/contacts/{id:guid}");

            Summary(s =>
            {
                s.Summary = "Updates an existing contact.";
                s.Description = "Updates the details of an existing contact identified by its unique ID.";
                s.Params["id"] = "The unique identifier (GUID) of the contact to update.";
                s.AddUpdateResponses<UpdateContactResponse>("Contat");
            });
        }

        public override async Task<Results<Ok<UpdateContactResponse>, NotFound, InternalServerError>> ExecuteAsync(UpdateContactRequest request, CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var dtoContact = request.MapToDto(id);

            var result = await _contactService.UpdateContactAsync(dtoContact, ct);

            var response = result.Match<Results<Ok<UpdateContactResponse>, NotFound, InternalServerError>>(
                updatedContact => TypedResults.Ok(updatedContact.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
