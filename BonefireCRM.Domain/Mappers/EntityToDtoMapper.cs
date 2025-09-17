using BonefireCRM.Domain.DTOs;
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
            };
        }

        internal static CreatedContactDTO MapToCreatedDto(this Contact contact)
        {
            return new()
            {
                Id = contact.Id,
                Email = contact.Email,
            };
        }

        internal static UpdatedContactDTO MapToUpdatedDto(this Contact contact)
        {
            return new()
            {
                Id = contact.Id,
                Email = contact.Email,
            };
        }
    }
}
