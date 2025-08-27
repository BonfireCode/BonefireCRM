using BonefireCRM.Domain.DTOs;
using BonefireCRM.Domain.Entities;

namespace BonefireCRM.Domain.Mappers
{
    internal static class DtoToEntityMapper
    {
        internal static Contact MapToContact(this GetContactDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                JobTitle = dto.JobTitle,
                PhoneNumber = dto.PhoneNumber,
            };
        }

        internal static Contact MapToContact(this CreateContactDTO dto)
        {
            return new()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                JobTitle = dto.JobTitle,
                PhoneNumber = dto.PhoneNumber,
            };
        }

        internal static Contact MapToContact(this UpdateContactDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                JobTitle = dto.JobTitle,
                PhoneNumber = dto.PhoneNumber,
            };
        }
    }
}
