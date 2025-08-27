using BonefireCRM.API.Contact.Mappers;
using BonefireCRM.API.Contrat;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Contact.Endpoints
{
    public class CreateContactEndpoint : Endpoint<CreateContactRequest, Results<Created<CreateContactResponse>, InternalServerError>>
    {
        private readonly ServiceContact _serviceContact;

        public CreateContactEndpoint(ServiceContact serviceContact)
        {
            _serviceContact = serviceContact;
        }

        public override void Configure()
        {
            Post("/contacts");
            AllowAnonymous();
        }

        public override async Task<Results<Created<CreateContactResponse>, InternalServerError>> ExecuteAsync(CreateContactRequest request, CancellationToken ct)
        {
            var dtoContact = RequestToDtoMapper.MapToDto(request);

            var result = await _serviceContact.CreateContactAsync(dtoContact, ct);

            var response = result.Match<Results<Created<CreateContactResponse>, InternalServerError>>
            (
                createdContact => TypedResults.Created($"/contacts/{createdContact.Id}", createdContact.MapToResponse()),
                _ => TypedResults.InternalServerError()
            );

            return response;
        }
    }
}
