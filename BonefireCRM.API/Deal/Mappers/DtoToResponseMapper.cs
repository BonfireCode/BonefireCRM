// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Deal;
using BonefireCRM.API.Contrat.Deal.Participant;
using BonefireCRM.Domain.DTOs.Deal;
using BonefireCRM.Domain.DTOs.Deal.Participant;

namespace BonefireCRM.API.Deal.Mappers
{
    internal static class DtoToResponseMapper
    {
        internal static GetDealsResponse MapToResponse(this GetDealsDTO dto)
        {
            return new()
            {
                Deals = dto.Deals.Select(d => d.MapToResponse()),
            };
        }

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
                DealParticipants = dto.DealParticipants?.Select(dp => dp.MapToResponse()),
            };
        }

        internal static DealSummaryResponse MapToResponse(this DealSummaryDTO dto)
        {
            return new ()
            {
                Id = dto.Id,
                Amount = dto.Amount,
                ExpectedCloseDate = dto.ExpectedCloseDate,
                PipelineStageId = dto.PipelineStageId,
                Title = dto.Title,
                CompanyId = dto.CompanyId,
                PrimaryContactId = dto.PrimaryContactId,
                UserId = dto.UserId,
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
                DealParticipants = dto.DealParticipants.Select(dp => dp.MapToResponse()),
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
                DealParticipants = dto.DealParticipants.Select(dp => dp.MapToResponse()),
            };
        }

        private static UpsertDealParticipantResponse MapToResponse(this UpsertedDealParticipantDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                DealParticipantRoleId = dto.DealParticipantRoleId,
                ContactId = dto.ContactId,
            };
        }

        private static GetDealParticipantResponse MapToResponse(this GetDealParticipantDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                DealParticipantRoleId = dto.DealParticipantRoleId,
                ContactId = dto.ContactId,
            };
        }

        private static CreatedDealParticipantResponse MapToResponse(this CreatedDealParticipantDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                DealParticipantRoleId = dto.DealParticipantRoleId,
                ContactId = dto.ContactId,
            };
        }
    }
}
