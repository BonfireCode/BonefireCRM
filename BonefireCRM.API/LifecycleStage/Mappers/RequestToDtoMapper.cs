// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.LifeCycleStage;
using BonefireCRM.Domain.Constants;
using BonefireCRM.Domain.DTOs.LifeCycleStage;

namespace BonefireCRM.API.LifecycleStage.Mappers
{
    internal static class RequestToDtoMapper
    {
        internal static GetAllLifecycleStagesDTO MapToDto(this GetLifecycleStagesRequest request)
        {
            return new()
            {
                Id = request.Id,
                Name = request.Name,
                SortBy = request.SortBy ?? nameof(request.Id),
                SortDirection = request.SortDirection ?? DefaultValues.SORTDIRECTION,
                PageNumber = request.PageNumber ?? DefaultValues.PAGENUMBER,
                PageSize = request.PageSize ?? DefaultValues.PAGESIZE,
            };
        }
    }
}
