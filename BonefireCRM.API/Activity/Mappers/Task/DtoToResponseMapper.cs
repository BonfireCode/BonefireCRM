// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Call;
using BonefireCRM.API.Contrat.Task;
using BonefireCRM.Domain.DTOs.Activity.Call;
using BonefireCRM.Domain.DTOs.Activity.Task;

namespace BonefireCRM.API.Company.Mappers.Task
{
    internal static class DtoToResponseMapper
    {
        internal static GetTaskResponse MapToResponse(this GetTaskDTO dto)
        {
            return new()
            {
            };
        }

        internal static CreateTaskResponse MapToResponse(this CreatedTaskDTO dto)
        {
            return new()
            {
            };
        }

        internal static UpdateTaskResponse MapToResponse(this UpdatedTaskDTO dto)
        {
            return new()
            {
            };
        }
    }
}
