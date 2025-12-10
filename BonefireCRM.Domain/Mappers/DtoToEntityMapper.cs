using BonefireCRM.Domain.DTOs.Activity.Assignment;
using BonefireCRM.Domain.DTOs.Activity.Call;
using BonefireCRM.Domain.DTOs.Activity.Meeting;
using BonefireCRM.Domain.DTOs.Company;
using BonefireCRM.Domain.DTOs.Contact;
using BonefireCRM.Domain.DTOs.Deal;
using BonefireCRM.Domain.DTOs.Deal.Participant;
using BonefireCRM.Domain.DTOs.DealParticipantRole;
using BonefireCRM.Domain.DTOs.Security;
using BonefireCRM.Domain.DTOs.User;
using BonefireCRM.Domain.Entities;

namespace BonefireCRM.Domain.Mappers
{
    internal static class DtoToEntityMapper
    {
        internal static Assignment MapToAssignment(this CreateAssignmentDTO dto)
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

        internal static Assignment MapToAssignment(this UpdateAssignmentDTO dto)
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

        internal static DealParticipant MapToDealParticipant(this CreateDealParticipantDTO dto)
        {
            return new()
            {
                ContactId = dto.ContactId,
                DealParticipantRoleId = dto.DealParticipantRoleId,
            };
        }

        internal static DealParticipant MapToDealParticipant(this UpsertDealParticipantDTO dto, Guid dealId)
        {
            return new()
            {
                Id = dto.Id,
                DealId = dealId,
                ContactId = dto.ContactId,
                DealParticipantRoleId = dto.DealParticipantRoleId,
            };
        }

        internal static DealParticipantRole MapToDealParticipantRole(this CreateDealParticipantRoleDTO dto)
        {
            return new()
            {
                Name = dto.Name,
                Description = dto.Description,
                RegisteredByUserId = dto.RegisteredByUserId,
            };
        }

        internal static DealParticipantRole MapToDealParticipantRole(this UpdateDealParticipantRoleDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
                RegisteredByUserId = dto.RegisteredByUserId,
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
                JobRole = dto.JobRole,
                UserId = dto.UserId,
                LifecycleStageId = dto.LifecycleStageId,
                CompanyId = dto.CompanyId == Guid.Empty ? null : dto.CompanyId,
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
                LifecycleStageId = dto.LifecycleStageId,
                UserId = dto.UserId,
                CompanyId = dto.CompanyId == Guid.Empty ? null : dto.CompanyId,
            };
        }

        internal static Deal MapToDeal(this CreateDealDTO dto)
        {
            return new()
            {
                Amount = dto.Amount,
                Title = dto.Title,
                ExpectedCloseDate = dto.ExpectedCloseDate,
                PipelineStageId = dto.PipelineStageId,
                CompanyId = dto.CompanyId,
                PrimaryContactId = dto.PrimaryContactId,
                UserId = dto.UserId,
                DealParticipants = dto.DealParticipants.Select( dp => dp.MapToDealParticipant()).ToList(),
            };
        }

        internal static Deal MapToDeal(this UpdateDealDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Amount = dto.Amount,
                Title = dto.Title,
                ExpectedCloseDate = dto.ExpectedCloseDate,
                PipelineStageId = dto.PipelineStageId,
                CompanyId = dto.CompanyId,
                PrimaryContactId = dto.PrimaryContactId,
                UserId = dto.UserId,
                DealParticipants = dto.DealParticipants.Select(dp => dp.MapToDealParticipant(dto.Id)).ToList(),
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
