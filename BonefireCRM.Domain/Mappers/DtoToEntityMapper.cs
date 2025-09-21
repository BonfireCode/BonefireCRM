using BonefireCRM.Domain.DTOs.Contact;
using BonefireCRM.Domain.DTOs.Security;
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
                PhoneNumber = dto.PhoneNumber,
                JobRole = dto.JobRole
            };
        }

        internal static Contact MapToContact(this CreateContactDTO dto)
        {
            return new()
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber,
                JobRole = dto.JobRole
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
                PhoneNumber = dto.PhoneNumber,
                JobRole = dto.JobRole
            };
        }

        internal static User MapToUser(this RegisterDTO dto, string registeredUserId)
        {
            return new()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                RegisterId = registeredUserId
            };
        }
    }
}
