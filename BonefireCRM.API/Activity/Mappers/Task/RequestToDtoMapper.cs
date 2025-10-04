// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Task;
using BonefireCRM.Domain.DTOs.Activity.Task;

namespace BonefireCRM.API.Company.Mappers.Task
{
    internal static class RequestToDtoMapper
    {
        internal static CreateTaskDTO MapToDto(CreateTaskRequest request)
        {
            return new()
            {
            };
        }

        internal static UpdateTaskDTO MapToDto(UpdateTaskRequest request, Guid id)
        {
            return new()
            {
            };
        }
    }
}
