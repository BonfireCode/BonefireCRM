// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.LifeCycleStage;
using BonefireCRM.Domain.DTOs.LifeCycleStage;

namespace BonefireCRM.API.LifecycleStage.Mappers
{
    internal static class DtoToResponseMapper
    {
        internal static GetLifecycleStageResponse MapToResponse(this GetLifecycleStageDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
            };
        }
    }
}
