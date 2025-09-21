using BonefireCRM.API.Contrat.Contact;
using BonefireCRM.Domain.DTOs.Contact;

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
                JobRole = request.JobRole,
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
                JobRole = request.JobRole,
                PhoneNumber = request.PhoneNumber,
            };
        }
    }
}
