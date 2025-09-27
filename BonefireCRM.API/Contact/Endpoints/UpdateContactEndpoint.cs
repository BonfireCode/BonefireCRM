// <copyright file="UpdateContactEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contact.Mappers;
using BonefireCRM.API.Contrat.Contact;
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
            AllowAnonymous();
        }

        public override async Task<Results<Ok<UpdateContactResponse>, NotFound, InternalServerError>> ExecuteAsync(UpdateContactRequest request, CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var dtoContact = RequestToDtoMapper.MapToDto(request);
            dtoContact.Id = id;

            var result = await _contactService.UpdateContactAsync(dtoContact, ct);

            var response = result.Match<Results<Ok<UpdateContactResponse>, NotFound, InternalServerError>>(
                updatedContact => TypedResults.Ok(updatedContact.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
