// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Task;
using BonefireCRM.Domain.DTOs.Activity.Task;

namespace BonefireCRM.API.Activity.Mappers.Task
{
    internal static class DtoToResponseMapper
    {
        internal static GetTaskResponse MapToResponse(this GetTaskDTO dto)
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

        internal static CreateTaskResponse MapToResponse(this CreatedTaskDTO dto)
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

        internal static UpdateTaskResponse MapToResponse(this UpdatedTaskDTO dto)
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
