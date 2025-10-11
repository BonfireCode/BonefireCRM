// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>
using BonefireCRM.API.Contrat.Meeting;
using BonefireCRM.Domain.DTOs.Activity.Meeting;

namespace BonefireCRM.API.Activity.Mappers.Meeting
{
    internal static class DtoToResponseMapper
    {
        internal static GetMeetingResponse MapToResponse(this GetMeetingDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                CompanyId = dto.CompanyId,
                ContactId = dto.ContactId,
                EndTime = dto.EndTime,
                StartTime = dto.StartTime,
                Notes = dto.Notes,
                DealId = dto.DealId,
                Subject = dto.Subject,
            };
        }

        internal static CreateMeetingResponse MapToResponse(this CreatedMeetingDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                CompanyId = dto.CompanyId,
                ContactId = dto.ContactId,
                EndTime = dto.EndTime,
                StartTime = dto.StartTime,
                Notes = dto.Notes,
                DealId = dto.DealId,
                Subject = dto.Subject,
            };
        }

        internal static UpdateMeetingResponse MapToResponse(this UpdatedMeetingDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                CompanyId = dto.CompanyId,
                ContactId = dto.ContactId,
                EndTime = dto.EndTime,
                StartTime = dto.StartTime,
                Notes = dto.Notes,
                DealId = dto.DealId,
                Subject = dto.Subject,
            };
        }
    }
}
