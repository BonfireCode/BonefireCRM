// <copyright file="CreateContactEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contact.Mappers;
using BonefireCRM.API.Contrat.Contact;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Contact.Endpoints
{
    public class CreateContactEndpoint : Endpoint<CreateContactRequest, Results<Created<CreateContactResponse>, InternalServerError>>
    {
        private readonly ContactService _contactService;

        public CreateContactEndpoint(ContactService contactService)
        {
            _contactService = contactService;
        }

        public override void Configure()
        {
            Post("/contacts");
            AllowAnonymous();
        }

        public override async Task<Results<Created<CreateContactResponse>, InternalServerError>> ExecuteAsync(CreateContactRequest request, CancellationToken ct)
        {
            var dtoContact = RequestToDtoMapper.MapToDto(request);

            var result = await _contactService.CreateContactAsync(dtoContact, ct);

            var response = result.Match<Results<Created<CreateContactResponse>, InternalServerError>>(
                createdContact => TypedResults.Created($"/contacts/{createdContact.Id}", createdContact.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
