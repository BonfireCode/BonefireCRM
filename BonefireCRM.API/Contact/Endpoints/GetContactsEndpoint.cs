// <copyright file="GetContactsEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contact.Mappers;
using BonefireCRM.API.Contrat.Contact;
using BonefireCRM.API.Contrat.Meeting;
using BonefireCRM.API.Contrat.Shared;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using LanguageExt.Common;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Contact.Endpoints
{
    public class GetContactsEndpoint : Endpoint<GetContactsRequest, Results<Ok<PagedResult<GetContactResponse>>, InternalServerError>>
    {
        private readonly ContactService _contactService;

        public GetContactsEndpoint(ContactService contactService)
        {
            _contactService = contactService;
        }

        public override void Configure()
        {
            Get("/contacts");
            AllowAnonymous();
        }

        public override async Task<Results<Ok<PagedResult<GetContactResponse>>, InternalServerError>> ExecuteAsync(GetContactsRequest request, CancellationToken ct)
        {
            var dtoContacts = request.MapToDto();
            var result = await _contactService.GetContactsAsync(dtoContacts, ct);

            var response = result.Match<Results<Ok<PagedResult<GetContactResponse>>, InternalServerError>>(
                contacts => TypedResults.Ok(contacts.MapToResponse()),
                _ => TypedResults.InternalServerError());
            return response;
        }
    }
}
