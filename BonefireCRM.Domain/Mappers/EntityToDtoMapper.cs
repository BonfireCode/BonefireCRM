using BonefireCRM.Domain.DTOs.Contact;
using BonefireCRM.Domain.Entities;

namespace BonefireCRM.Domain.Mappers
{
    internal static class EntityToDtoMapper
    {
        internal static GetContactDTO? MapToGetDto(this Contact contact)
        {
            return new GetContactDTO
            {
                Id = contact.Id,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                JobTitle = contact.JobTitle,
                PhoneNumber = contact.PhoneNumber,
            };
        }

        internal static CreatedContactDTO MapToCreatedDto(this Contact contact)
        {
            return new()
            {
                Id = contact.Id,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                JobTitle = contact.JobTitle,
                PhoneNumber = contact.PhoneNumber,
            };
        }

        internal static UpdatedContactDTO MapToUpdatedDto(this Contact contact)
        {
            return new()
            {
                Id = contact.Id,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                JobTitle = contact.JobTitle,
                PhoneNumber = contact.PhoneNumber,
            };
        }
    }
}
