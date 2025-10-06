using BonefireCRM.Domain.DTOs.Activity.Call;
using BonefireCRM.Domain.DTOs.Activity.Meeting;
using BonefireCRM.Domain.DTOs.Activity.Task;
using BonefireCRM.Domain.DTOs.Company;
using BonefireCRM.Domain.DTOs.Contact;
using BonefireCRM.Domain.DTOs.Security;
using BonefireCRM.Domain.Entities;

namespace BonefireCRM.Domain.Mappers
{
    internal static class DtoToEntityMapper
    {
        internal static Assignment MapToAssignment(this GetTaskDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                CompanyId = dto.CompanyId,
                DealId = dto.DealId,
                UserId = dto.UserId,
                ContactId = dto.ContactId,
                Subject = dto.Subject,
                Description = dto.Description,
                DueDate = dto.DueDate,
                IsCompleted = dto.IsCompleted,
            };
        }

        internal static Assignment MapToAssignment(this CreateTaskDTO dto)
        {
            return new()
            {
                CompanyId = dto.CompanyId,
                DealId = dto.DealId,
                UserId = dto.UserId,
                ContactId = dto.ContactId,
                Subject = dto.Subject,
                Description = dto.Description,
                DueDate = dto.DueDate,
                IsCompleted = dto.IsCompleted,
            };
        }

        internal static Assignment MapToAssignment(this UpdateTaskDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                CompanyId = dto.CompanyId,
                DealId = dto.DealId,
                UserId = dto.UserId,
                ContactId = dto.ContactId,
                Subject = dto.Subject,
                Description = dto.Description,
                DueDate = dto.DueDate,
                IsCompleted = dto.IsCompleted,
            };
        }

        internal static Meeting MapToMeeting(this GetMeetingDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                CompanyId = dto.CompanyId,
                DealId = dto.DealId,
                UserId = dto.UserId,
                ContactId = dto.ContactId,
                EndTime = dto.EndTime,
                StartTime = dto.StartTime,
                Subject = dto.Subject,
                Notes = dto.Notes,
            };
        }

        internal static Meeting MapToMeeting(this CreateMeetingDTO dto)
        {
            return new()
            {
                CompanyId = dto.CompanyId,
                DealId = dto.DealId,
                UserId = dto.UserId,
                ContactId = dto.ContactId,
                EndTime = dto.EndTime,
                StartTime = dto.StartTime,
                Subject = dto.Subject,
                Notes = dto.Notes,
            };
        }

        internal static Meeting MapToMeeting(this UpdateMeetingDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                CompanyId = dto.CompanyId,
                DealId = dto.DealId,
                UserId = dto.UserId,
                ContactId = dto.ContactId,
                EndTime = dto.EndTime,
                StartTime = dto.StartTime,
                Subject = dto.Subject,
                Notes = dto.Notes,
            };
        }

        internal static Call MapToCall(this GetCallDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                CompanyId = dto.CompanyId,
                DealId = dto.DealId,
                UserId = dto.UserId,
                ContactId = dto.ContactId,
                CallTime = dto.CallTime,
                Duration = dto.Duration,
                Notes = dto.Notes,

            };
        }

        internal static Call MapToCall(this CreateCallDTO dto)
        {
            return new()
            {
                CompanyId = dto.CompanyId,
                DealId = dto.DealId,
                UserId = dto.UserId,
                ContactId = dto.ContactId,
                CallTime = dto.CallTime,
                Duration = dto.Duration,
                Notes = dto.Notes,
            };
        }

        internal static Call MapToCall(this UpdateCallDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                CompanyId = dto.CompanyId,
                DealId = dto.DealId,
                UserId = dto.UserId,
                ContactId = dto.ContactId,
                CallTime = dto.CallTime,
                Duration = dto.Duration,
                Notes = dto.Notes,
            };
        }

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
                JobRole = dto.JobRole,
            };
        }

        internal static Company MapToCompany(this GetCompanyDTO dto)
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
