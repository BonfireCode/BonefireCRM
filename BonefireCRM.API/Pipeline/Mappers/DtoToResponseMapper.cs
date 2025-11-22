// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Pipeline;
using BonefireCRM.Domain.DTOs.Pipeline;

namespace BonefireCRM.API.Pipeline.Mappers
{
    internal static class DtoToResponseMapper
    {
        internal static GetPipelineResponse MapToResponse(this GetPipelineDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
                IsDefault = dto.IsDefault,
            };
        }
    }
}
