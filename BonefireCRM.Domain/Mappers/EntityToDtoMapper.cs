using BonefireCRM.Domain.DTOs.Company;
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
                PhoneNumber = contact.PhoneNumber,
                JobRole = contact.JobRole
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
                PhoneNumber = contact.PhoneNumber,
                JobRole = contact.JobRole
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
                PhoneNumber = contact.PhoneNumber,
                JobRole = contact.JobRole
            };
        }

        internal static GetCompanyDTO? MapToGetDto(this Company Company)
        {
            return new GetCompanyDTO
            {
                Id = Company.Id,
                Name = Company.Name,
                Industry = Company.Industry,
                Address = Company.Address,
                PhoneNumber = Company.PhoneNumber,
            };
        }

        internal static CreatedCompanyDTO MapToCreatedDto(this Company Company)
        {
            return new()
            {
                Id = Company.Id,
                Name = Company.Name,
                Industry = Company.Industry,
                Address = Company.Address,
                PhoneNumber = Company.PhoneNumber,
            };
        }

        internal static UpdatedCompanyDTO MapToUpdatedDto(this Company Company)
        {
            return new()
            {
                Id = Company.Id,
                Name = Company.Name,
                Industry = Company.Industry,
                Address = Company.Address,
                PhoneNumber = Company.PhoneNumber,
            };
        }
    }
}
