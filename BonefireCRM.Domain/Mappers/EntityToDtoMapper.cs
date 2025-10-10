using BonefireCRM.Domain.DTOs.Company;
using BonefireCRM.Domain.DTOs.Contact;
using BonefireCRM.Domain.DTOs.User;
using BonefireCRM.Domain.Entities;

namespace BonefireCRM.Domain.Mappers
{
    internal static class EntityToDtoMapper
    {
        internal static GetContactDTO MapToGetDto(this Contact contact)
        {
            return new GetContactDTO
            {
                Id = contact.Id,
                Email = contact.Email,
                FirstName = contact.FirstName,
                LastName = contact.LastName,
                PhoneNumber = contact.PhoneNumber,
                JobRole = contact.JobRole,
                LifecycleStageId = contact.LifecycleStageId,
                CompanyId = contact.CompanyId ?? Guid.Empty,
                UserId = contact.UserId,
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
                JobRole = contact.JobRole,
                LifecycleStageId = contact.LifecycleStageId,
                CompanyId = contact.CompanyId ?? Guid.Empty,
                UserId = contact.UserId,
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
                JobRole = contact.JobRole,
                LifecycleStageId = contact.LifecycleStageId,
                CompanyId = contact.CompanyId ?? Guid.Empty,
                UserId = contact.UserId,
            };
        }

        internal static GetCompanyDTO MapToGetDto(this Company Company)
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

        internal static GetUserDTO MapToGetDto(this User user)
        {
            return new GetUserDTO
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Contacts = user.Contacts.Select(c => c.MapToGetDto()),
                //Activities = user.Activities.Select(a => a.MapToGetDto())
            };
        }

        internal static UpdatedUserDTO MapToUpdatedDto(this User user)
        {
            return new()
            {
                Id = user.Id,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Contacts = user.Contacts.Select(c => c.MapToUpdatedDto()),
                //Activities = user.Activities.Select(a => a.MapToUpdatedDto())
            };
        }
    }
}
