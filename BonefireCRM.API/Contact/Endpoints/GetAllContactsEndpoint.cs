// <copyright file="GetAllContactsEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contact.Mappers;
using BonefireCRM.API.Contrat.Contact;
using BonefireCRM.API.Extensions;
using BonefireCRM.Domain.Services;
using FastEndpoints;

namespace BonefireCRM.API.Contact.Endpoints
{
    public class GetAllContactsEndpoint : Endpoint<GetContactsRequest, IEnumerable<GetContactResponse>>
    {
        private readonly ContactService _contactService;

        public GetAllContactsEndpoint(ContactService contactService)
        {
            _contactService = contactService;
        }

        public override void Configure()
        {
            Get("/contacts");

            Summary(s =>
            {
                s.Summary = "Retrieves all specific contacts";
                s.Description = "Fetches detailed information about contacts";

                s.AddGetAllResponses<IEnumerable<GetContactResponse>>("Contacts");
            });
        }

        public override async Task<IEnumerable<GetContactResponse>> ExecuteAsync(GetContactsRequest request, CancellationToken ct)
        {
            var dtoContacts = request.MapToDto();

            var result = _contactService.GetAllContacts(dtoContacts, ct);

            var response = result.Select(c => c.MapToResponse());

            return await Task.Run(() => response);
        }
    }
}
