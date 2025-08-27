using BonefireCRM.API.Contact.Mappers;
using BonefireCRM.Domain.Services;
using FastEndpoints;
using Microsoft.AspNetCore.Http.HttpResults;

namespace BonefireCRM.API.Contact.Endpoints
{
    public class DeleteContactEndpoint : EndpointWithoutRequest<Results<NoContent, NotFound>>
    {
        private readonly ServiceContact _serviceContact;

        public DeleteContactEndpoint(ServiceContact serviceContact)
        {
            _serviceContact = serviceContact;
        }

        public override void Configure()
        {
            Delete("/contacts/{id}");
            AllowAnonymous();
        }

        public override async Task<Results<NoContent, NotFound>> ExecuteAsync(CancellationToken ct)
        {
            var id = Route<Guid>("id");

            var isDeleted = await _serviceContact.DeleteContactAsync(id, ct);
            if (isDeleted)
            {
                return TypedResults.NoContent();
            }

            return TypedResults.NotFound();
        }
    }
}
