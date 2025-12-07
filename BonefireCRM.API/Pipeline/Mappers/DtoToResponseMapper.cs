// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Pipeline;
using BonefireCRM.API.Contrat.Pipeline.Stage;
using BonefireCRM.Domain.DTOs.Pipeline;
using BonefireCRM.Domain.DTOs.Pipeline.Stage;

namespace BonefireCRM.API.Pipeline.Mappers
{
    internal static class DtoToResponseMapper
    {
        internal static GetPipelineListItemResponse MapToResponse(this GetPipelineListItemDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
                IsDefault = dto.IsDefault,
            };
        }

        internal static GetPipelineResponse MapToResponse(this GetPipelineDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
                IsDefault = dto.IsDefault,
                PipelineStages = dto.PipelineStages?.Select(ps => ps.MapToResponse()),
            };
        }

        private static GetPipelineStageResponse MapToResponse(this GetPipelineStageDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
                OrderIndex = dto.OrderIndex,
                Status = dto.Status?.ToString(),
            };
        }
    }
}
