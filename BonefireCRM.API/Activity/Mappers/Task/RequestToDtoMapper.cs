﻿// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Task;
using BonefireCRM.Domain.DTOs.Activity.Task;

namespace BonefireCRM.API.Activity.Mappers.Task
{
    internal static class RequestToDtoMapper
    {
        internal static CreateTaskDTO MapToDto(this CreateTaskRequest request, Guid userId)
        {
            return new()
            {
                CompanyId = request.CompanyId,
                ContactId = request.ContactId,
                DealId = request.DealId,
                Subject = request.Subject,
                Description = request.Description,
                DueDate = request.DueDate,
                IsCompleted = request.IsCompleted,
                UserId = userId,
            };
        }

        internal static UpdateTaskDTO MapToDto(this UpdateTaskRequest request, Guid id)
        {
            return new()
            {
                Id = id,
                CompanyId = request.CompanyId,
                ContactId = request.ContactId,
                DealId = request.DealId,
                Subject = request.Subject,
                Description = request.Description,
                DueDate = request.DueDate,
                IsCompleted = request.IsCompleted,
            };
        }
    }
}
