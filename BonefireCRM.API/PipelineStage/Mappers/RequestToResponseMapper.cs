// <copyright file="RequestToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.PipelineStage;
using BonefireCRM.Domain.DTOs.PipelineStage;
using BonefireCRM.Domain.Enums;

namespace BonefireCRM.API.PipelineStage.Mappers
{
    internal static class RequestToResponseMapper
    {
        internal static GetAllPipelineStagesDTO MapToDto(this GetPipelineStagesRequest request, Guid pipelineId)
        {
            return new()
            {
                Id = request.Id,
                PipelineId = pipelineId,
                Name = request.Name,
                OrderIndex = request.OrderIndex,
                Status = Enum.TryParse<DealClosureStatus>(request.Status, true, out var s) ? s : null,
                SortBy = request.SortBy ?? nameof(request.Id),
                SortDirection = request.SortDirection ?? Domain.Constants.DefaultValues.SORTDIRECTION,
                PageNumber = request.PageNumber ?? Domain.Constants.DefaultValues.PAGENUMBER,
                PageSize = request.PageSize ?? Domain.Constants.DefaultValues.PAGESIZE,
            };
        }
    }
}
