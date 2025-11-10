// <copyright file="GetContactsEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contact.Mappers;
using BonefireCRM.API.Contrat.Contact;
using BonefireCRM.API.Contrat.Shared;
using BonefireCRM.Domain.Services;
using FastEndpoints;

namespace BonefireCRM.API.Contact.Endpoints
{
    public class GetContactsEndpoint : Endpoint<FilterRequest, PaginatedResult<GetContactResponse>>
    {
        private readonly ContactService _contactService;

        public GetContactsEndpoint(ContactService contactService)
        {
            _contactService = contactService;
        }

        public override void Configure()
        {
            Post("/contacts/filter");
            AllowAnonymous();
        }

        public override async Task<PaginatedResult<GetContactResponse>> ExecuteAsync(FilterRequest request, CancellationToken ct)
        {
            var dtoContacts = request.MapToDto();
            var response = await _contactService.GetContactsAsync(dtoContacts, ct);
            return response.MapToResponse();
        }
    }
}
