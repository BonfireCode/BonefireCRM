using BonefireCRM.API.Contrat;
using BonefireCRM.Domain.DTOs;

namespace BonefireCRM.API.Contact.Mappers
{
    internal static class RequestToDtoMapper
    {
        internal static CreateContactDTO MapToDto(CreateContactRequest request)
        {
            return new()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                JobTitle = request.JobTitle,
                PhoneNumber = request.PhoneNumber,
            };
        }

        internal static UpdateContactDTO MapToDto(UpdateContactRequest request)
        {
            return new()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                JobTitle = request.JobTitle,
                PhoneNumber = request.PhoneNumber,
            };
        }
    }
}
