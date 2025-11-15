// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Activity.Assignment;
using BonefireCRM.Domain.DTOs.Activity.Assignment;

namespace BonefireCRM.API.Activity.Mappers.Task
{
    internal static class DtoToResponseMapper
    {
        internal static GetAssignmentResponse MapToResponse(this GetAssignmentDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                CompanyId = dto.CompanyId,
                DealId = dto.DealId,
                ContactId = dto.ContactId,
                Subject = dto.Subject,
                Description = dto.Description,
                DueDate = dto.DueDate,
                IsCompleted = dto.IsCompleted,
            };
        }

        internal static CreateAssignmentResponse MapToResponse(this CreatedAssignmentDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                CompanyId = dto.CompanyId,
                DealId = dto.DealId,
                ContactId = dto.ContactId,
                Subject = dto.Subject,
                Description = dto.Description,
                DueDate = dto.DueDate,
                IsCompleted = dto.IsCompleted,
            };
        }

        internal static UpdateAssignmentResponse MapToResponse(this UpdatedAssignmentDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                CompanyId = dto.CompanyId,
                DealId = dto.DealId,
                ContactId = dto.ContactId,
                Subject = dto.Subject,
                Description = dto.Description,
                DueDate = dto.DueDate,
                IsCompleted = dto.IsCompleted,
            };
        }
    }
}
