using BonefireCRM.API.Contact.Mappers;
using BonefireCRM.API.Contrat;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using LanguageExt;
using LanguageExt.ClassInstances;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Contact.Endpoints
{
    public class UpdateContactEndpoint : Endpoint<UpdateContactRequest, Results<Ok<UpdateContactResponse>, NotFound, InternalServerError>>
    {
        private readonly ServiceContact _serviceContact;

        public UpdateContactEndpoint(ServiceContact serviceContact)
        {
            _serviceContact = serviceContact;
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

            var result = await _serviceContact.UpdateContactAsync(dtoContact, ct);

            var response = result.Match<Results<Ok<UpdateContactResponse>, NotFound, InternalServerError>>
            (
                updatedContact => TypedResults.Ok(updatedContact.MapToResponse()),
                _ => TypedResults.InternalServerError()
            );

            return response;
        }
    }
}
