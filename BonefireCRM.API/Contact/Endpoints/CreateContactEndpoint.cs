// <copyright file="CreateContactEndpoint.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contact.Mappers;
using BonefireCRM.API.Contrat.Contact;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Security.Claims;

namespace BonefireCRM.API.Contact.Endpoints
{
    public class CreateContactEndpoint : Endpoint<CreateContactRequest, Results<Created<CreateContactResponse>, InternalServerError>>
    {
        private readonly ContactService _contactService;
        private readonly UserService _userService;

        public CreateContactEndpoint(ContactService contactService, UserService userService)
        {
            _contactService = contactService;
            _userService = userService;
        }

        public override void Configure()
        {
            Post("/contacts");
        }

        public override async Task<Results<Created<CreateContactResponse>, InternalServerError>> ExecuteAsync(CreateContactRequest request, CancellationToken ct)
        {
            var registerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var userId = await _userService.GetUserIdAsync(registerId, ct);

            var dtoContact = request.MapToDto(userId);

            var result = await _contactService.CreateContactAsync(dtoContact, ct);

            var response = result.Match<Results<Created<CreateContactResponse>, InternalServerError>>(
                createdContact => TypedResults.Created($"/contacts/{createdContact.Id}", createdContact.MapToResponse()),
                _ => TypedResults.InternalServerError());

            return response;
        }
    }
}
