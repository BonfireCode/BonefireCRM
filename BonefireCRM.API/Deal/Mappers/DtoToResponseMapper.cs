// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Deal;
using BonefireCRM.Domain.DTOs.Deal;

namespace BonefireCRM.API.Deal.Mappers
{
    internal static class DtoToResponseMapper
    {
        internal static GetDealResponse MapToResponse(this GetDealDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                CompanyId = dto.CompanyId,
                UserId = dto.UserId,
                Amount = dto.Amount,
                ExpectedCloseDate = dto.ExpectedCloseDate,
                PipelineStageId = dto.PipelineStageId,
                PrimaryContactId = dto.PrimaryContactId,
                Title = dto.Title,
            };
        }

        internal static CreateDealResponse MapToResponse(this CreatedDealDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                UserId = dto.UserId,
                CompanyId = dto.CompanyId,
                PrimaryContactId = dto.PrimaryContactId,
                Amount = dto.Amount,
                ExpectedCloseDate = dto.ExpectedCloseDate,
                PipelineStageId = dto.PipelineStageId,
                Title = dto.Title,
            };
        }

        internal static UpdateDealResponse MapToResponse(this UpdatedDealDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                CompanyId = dto.CompanyId,
                UserId = dto.UserId,
                Amount = dto.Amount,
                ExpectedCloseDate = dto.ExpectedCloseDate,
                PipelineStageId = dto.PipelineStageId,
                PrimaryContactId = dto.PrimaryContactId,
                Title = dto.Title,
            };
        }
    }
}
