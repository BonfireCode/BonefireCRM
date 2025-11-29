using BonefireCRM.Domain.DTOs.Activity.Assignment;
using BonefireCRM.Domain.DTOs.Activity.Call;
using BonefireCRM.Domain.DTOs.Activity.Meeting;
using BonefireCRM.Domain.DTOs.Company;
using BonefireCRM.Domain.DTOs.Contact;
using BonefireCRM.Domain.DTOs.Deal;
using BonefireCRM.Domain.DTOs.DealParticipantRole;
using BonefireCRM.Domain.DTOs.LifeCycleStage;
using BonefireCRM.Domain.DTOs.Pipeline;
using BonefireCRM.Domain.DTOs.PipelineStage;
using BonefireCRM.Domain.DTOs.User;
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

        internal static GetAssignmentDTO? MapToGetDto(this Assignment assignment)
        {
            return new GetAssignmentDTO
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

        internal static CreatedAssignmentDTO MapToCreatedDto(this Assignment assignment)
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

        internal static UpdatedAssignmentDTO MapToUpdatedDto(this Assignment assignment)
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

        internal static GetLifecycleStageDTO MapToGetDto(this LifecycleStage lifecycleStage)
        {
            return new()
            {
                Id = lifecycleStage.Id,
                Name = lifecycleStage.Name,
            };
        }

        internal static GetDealParticipantRoleDTO MapToGetDto(this DealParticipantRole dealParticipantRole)
        {
            return new()
            {
                Id = dealParticipantRole.Id,
                Name = dealParticipantRole.Name,
                Description = dealParticipantRole.Description,
            };
        }

        internal static CreatedDealParticipantRoleDTO MapToCreatedDto(this DealParticipantRole dealParticipantRole)
        {
            return new()
            {
                Id = dealParticipantRole.Id,
                Description = dealParticipantRole.Description,
                Name = dealParticipantRole.Name,
            };
        }

        internal static UpdatedDealParticipantRoleDTO MapToUpdatedDto(this DealParticipantRole dealParticipantRole)
        {
            return new()
            {
                Id = dealParticipantRole.Id,
                Description = dealParticipantRole.Description,
                Name = dealParticipantRole.Name,
                RegisteredByUserId = dealParticipantRole.RegisteredByUserId,
            };
        }

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

        internal static GetDealDTO MapToGetDto(this Deal deal)
        {
            return new()
            { 
                Id = deal.Id,
                Amount = deal.Amount,
                ExpectedCloseDate = deal.ExpectedCloseDate,
                Title = deal.Title,
                PipelineStageId = deal.PipelineStageId,
                CompanyId = deal.CompanyId,
                PrimaryContactId = deal.PrimaryContactId,
                UserId = deal.UserId,
            };
        }

        internal static CreatedDealDTO MapToCreatedDto(this Deal deal)
        {
            return new()
            {
                Id = deal.Id,
                Amount = deal.Amount,
                ExpectedCloseDate = deal.ExpectedCloseDate,
                Title = deal.Title,
                PipelineStageId = deal.PipelineStageId,
                CompanyId = deal.CompanyId,
                PrimaryContactId = deal.PrimaryContactId,
                UserId = deal.UserId,
            };
        }

        internal static UpdatedDealDTO MapToUpdatedDto(this Deal deal)
        {
            return new()
            {
                Id = deal.Id,
                Amount = deal.Amount,
                ExpectedCloseDate = deal.ExpectedCloseDate,
                Title = deal.Title,
                PipelineStageId = deal.PipelineStageId,
                CompanyId = deal.CompanyId,
                PrimaryContactId = deal.PrimaryContactId,
                UserId = deal.UserId,
            };
        }

        internal static GetPipelineDTO MapToGetDto(this Pipeline pipeline)
        {
            return new GetPipelineDTO
            {
                Id = pipeline.Id,
                Name = pipeline.Name,
                IsDefault = pipeline.IsDefault,
            };
        }

        internal static AssignedDealParticipantDTO MapToAssignedDto(this DealParticipant deal)
        {
            return new()
            {
                DealId = deal.DealId,
                ContactId = deal.ContactId,
                DealParticipantRoleId = deal.DealParticipantRoleId,
            };
        }

        internal static GetDealParticipantDTO MapToGetDto(this DealParticipant deal)
        {
            return new()
            {
                DealId = deal.DealId,
                ContactId = deal.ContactId,
                DealParticipantRoleId = deal.DealParticipantRoleId,
            };
        }

        internal static GetPipelineStageDTO MapToGetDto(this PipelineStage pipelineStage)
        {
            return new GetPipelineStageDTO
            {
                Id = pipelineStage.Id,
                PipelineId = pipelineStage.PipelineId,
                Name = pipelineStage.Name,
                OrderIndex = pipelineStage.OrderIndex,
                Status = pipelineStage.Status,
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
