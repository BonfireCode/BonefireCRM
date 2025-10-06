using BonefireCRM.Domain.DTOs.Activity.Call;
using BonefireCRM.Domain.DTOs.Activity.Meeting;
using BonefireCRM.Domain.DTOs.Activity.Task;
using BonefireCRM.Domain.DTOs.Company;
using BonefireCRM.Domain.DTOs.Contact;
using BonefireCRM.Domain.Entities;

namespace BonefireCRM.Domain.Mappers
{
    internal static class EntityToDtoMapper
    {
        internal static GetCallDTO? MapToGetDto(this Call call)
        {
            return new GetCallDTO
            {
                Id = call.Id,
                CompanyId = call.CompanyId,
                DealId = call.DealId,
                UserId = call.UserId,
                ContactId = call.ContactId,
                CallTime = call.CallTime,
                Duration = call.Duration,
                Notes = call.Notes,
            };
        }

        internal static CreatedCallDTO MapToCreatedDto(this Call call)
        {
            return new()
            {
                Id = call.Id,
                CompanyId = call.CompanyId,
                DealId = call.DealId,
                UserId = call.UserId,
                ContactId = call.ContactId,
                CallTime = call.CallTime,
                Duration = call.Duration,
                Notes = call.Notes,
            };
        }

        internal static UpdatedCallDTO MapToUpdatedDto(this Call call)
        {
            return new()
            {
                Id = call.Id,
                CompanyId = call.CompanyId,
                DealId = call.DealId,
                UserId = call.UserId,
                ContactId = call.ContactId,
                CallTime = call.CallTime,
                Duration = call.Duration,
                Notes = call.Notes,
            };
        }

        internal static GetMeetingDTO? MapToGetDto(this Meeting meeting)
        {
            return new GetMeetingDTO
            {
                Id = meeting.Id,
                CompanyId = meeting.CompanyId,
                DealId = meeting.DealId,
                UserId = meeting.UserId,
                ContactId = meeting.ContactId,
                EndTime = meeting.EndTime,
                StartTime = meeting.StartTime,
                Subject = meeting.Subject,
                Notes = meeting.Notes,
            };
        }

        internal static CreatedMeetingDTO MapToCreatedDto(this Meeting meeting)
        {
            return new ()
            {
                Id = meeting.Id,
                CompanyId = meeting.CompanyId,
                DealId = meeting.DealId,
                UserId = meeting.UserId,
                ContactId = meeting.ContactId,
                EndTime = meeting.EndTime,
                StartTime = meeting.StartTime,
                Subject = meeting.Subject,
                Notes = meeting.Notes,
            };
        }

        internal static UpdatedMeetingDTO MapToUpdatedDto(this Meeting meeting)
        {
            return new ()
            {
                Id = meeting.Id,
                CompanyId = meeting.CompanyId,
                DealId = meeting.DealId,
                UserId = meeting.UserId,
                ContactId = meeting.ContactId,
                EndTime = meeting.EndTime,
                StartTime = meeting.StartTime,
                Subject = meeting.Subject,
                Notes = meeting.Notes,
            };
        }

        internal static GetTaskDTO? MapToGetDto(this Assignment assignment)
        {
            return new GetTaskDTO
            {
                Id = assignment.Id,
                CompanyId = assignment.CompanyId,
                DealId = assignment.DealId,
                UserId = assignment.UserId,
                ContactId = assignment.ContactId,
                Subject = assignment.Subject,
                Description = assignment.Description,
                DueDate = assignment.DueDate,
                IsCompleted = assignment.IsCompleted,
            };
        }

        internal static CreatedTaskDTO MapToCreatedDto(this Assignment assignment)
        {
            return new()
            {
                Id = assignment.Id,
                CompanyId = assignment.CompanyId,
                DealId = assignment.DealId,
                UserId = assignment.UserId,
                ContactId = assignment.ContactId,
                Subject = assignment.Subject,
                Description = assignment.Description,
                DueDate = assignment.DueDate,
                IsCompleted = assignment.IsCompleted,
            };
        }

        internal static UpdatedTaskDTO MapToUpdatedDto(this Assignment assignment)
        {
            return new()
            {
                Id = assignment.Id,
                CompanyId = assignment.CompanyId,
                DealId = assignment.DealId,
                UserId = assignment.UserId,
                ContactId = assignment.ContactId,
                Subject = assignment.Subject,
                Description = assignment.Description,
                DueDate = assignment.DueDate,
                IsCompleted = assignment.IsCompleted,
            };
        }

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
