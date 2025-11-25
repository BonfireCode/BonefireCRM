// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Deal;
using BonefireCRM.Domain.Constants;
using BonefireCRM.Domain.DTOs.Deal;

namespace BonefireCRM.API.Deal.Mappers
{
    internal static class RequestToDtoMapper
    {
        internal static GetAllDealsDTO MapToDto(this GetDealsRequest request, Guid userId)
        {
            return new()
            {
                Id = request.Id,
                Title = request.Title,
                Amount = request.Amount,
                ExpectedCloseDate = request.ExpectedCloseDate,
                PipelineStageId = request.PipelineStageId,
                PrimaryContactId = request.PrimaryContactId,
                UserId = userId,
                CompanyId = request.CompanyId,
                SortBy = request.SortBy ?? nameof(request.Id),
                SortDirection = request.SortDirection ?? DefaultValues.SORTDIRECTION,
                PageNumber = request.PageNumber ?? DefaultValues.PAGENUMBER,
                PageSize = request.PageSize ?? DefaultValues.PAGESIZE,
            };
        }

        internal static CreateDealDTO MapToDto(this CreateDealRequest request, Guid userId)
        {
            return new()
            {
                Title = request.Title,
                Amount = request.Amount,
                ExpectedCloseDate = request.ExpectedCloseDate,
                PipelineStageId = request.PipelineStageId,
                PrimaryContactId = request.PrimaryContactId,
                CompanyId = request.CompanyId,
                UserId = userId,
            };
        }

        internal static UpdateDealDTO MapToDto(this UpdateDealRequest request, Guid id, Guid userId)
        {
            return new()
            {
                Id = id,
                UserId = userId,
                CompanyId = request.CompanyId,
                Title = request.Title,
                Amount = request.Amount,
                ExpectedCloseDate = request.ExpectedCloseDate,
                PipelineStageId = request.PipelineStageId,
                PrimaryContactId = request.PrimaryContactId,
            };
        }
    }
}
