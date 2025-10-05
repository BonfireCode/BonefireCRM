using BonefireCRM.Domain.DTOs.Company;
using BonefireCRM.Domain.DTOs.Contact;
using BonefireCRM.Domain.DTOs.Security;
using BonefireCRM.Domain.DTOs.User;
using BonefireCRM.Domain.Entities;

namespace BonefireCRM.Domain.Mappers
{
    internal static class DtoToEntityMapper
    {
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
                JobRole = dto.JobRole,
            };
        }

        internal static Company MapToCompany(this CreateCompanyDTO dto)
        {
            return new()
            {
                Name = dto.Name,
                Industry = dto.Industry,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
            };
        }

        internal static Company MapToCompany(this UpdateCompanyDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Industry = dto.Industry,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
            };
        }

        internal static User MapToUser(this RegisterDTO dto, Guid registeredUserId)
        {
            return new()
            {
                UserName = dto.UserName,
                Email = dto.Email,
                RegisterId = registeredUserId
            };
        }

        internal static User MapToUser(this UpdateUserDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                RegisterId = dto.RegisterId,
            };
        }
    }
}
