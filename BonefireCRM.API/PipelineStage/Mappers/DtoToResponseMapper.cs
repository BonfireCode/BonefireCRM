// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.PipelineStage;
using BonefireCRM.Domain.DTOs.PipelineStage;

namespace BonefireCRM.API.PipelineStage.Mappers
{
    internal static class DtoToResponseMapper
    {
        internal static GetPipelineStageResponse MapToResponse(this GetPipelineStageDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
                OrderIndex = dto.OrderIndex,
                PipelineId = dto.PipelineId,
                Status = dto.Status?.ToString(),
            };
        }
    }
}
