// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Meeting;
using BonefireCRM.Domain.DTOs.Activity.Meeting;

namespace BonefireCRM.API.Activity.Mappers.Meeting
{
    internal static class RequestToDtoMapper
    {
        internal static CreateMeetingDTO MapToDto(this CreateMeetingRequest request, Guid userId)
        {
            return new()
            {
                EndTime = request.EndTime,
                StartTime = request.StartTime,
                Notes = request.Notes,
                Subject = request.Subject,
                CompanyId = request.CompanyId,
                ContactId = request.ContactId,
                DealId = request.DealId,
                UserId = userId,
            };
        }

        internal static UpdateMeetingDTO MapToDto(this UpdateMeetingRequest request, Guid id)
        {
            return new()
            {
                Id = id,
                EndTime = request.EndTime,
                StartTime = request.StartTime,
                Notes = request.Notes,
                Subject = request.Subject,
                CompanyId = request.CompanyId,
                ContactId = request.ContactId,
                DealId = request.DealId,
            };
        }
    }
}
