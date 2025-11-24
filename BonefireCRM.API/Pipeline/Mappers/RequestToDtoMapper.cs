// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Pipeline;
using BonefireCRM.Domain.Constants;
using BonefireCRM.Domain.DTOs.Pipeline;

namespace BonefireCRM.API.Pipeline.Mappers
{
    internal static class RequestToDtoMapper
    {
        internal static GetAllPipelinesDTO MapToDto(this GetPipelinesRequest request)
        {
            return new()
            {
                Id = request.Id,
                Name = request.Name,
                IsDefault = request.IsDefault,
                SortBy = request.SortBy ?? nameof(request.Id),
                SortDirection = request.SortDirection ?? DefaultValues.SORTDIRECTION,
                PageNumber = request.PageNumber ?? DefaultValues.PAGENUMBER,
                PageSize = request.PageSize ?? DefaultValues.PAGESIZE,
            };
        }
    }
}