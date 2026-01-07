// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Meeting;
using BonefireCRM.Domain.Constants;
using BonefireCRM.Domain.DTOs.Activity.Meeting;
using BonefireCRM.Domain.Entities;

namespace BonefireCRM.API.Activity.Mappers.Meeting
{
    internal static class RequestToDtoMapper
    {
        internal static GetAllMeetingsDTO MapToDto(this GetMeetingsRequest request)
        {
            return new()
            {
                Id = request.Id,
                Subject = request.Subject,
                Notes = request.Notes,
                StartTime = request.StartTime,
                EndTime = request.EndTime,
                UserId = request.UserId,
                ContactId = request.ContactId,
                CompanyId = request.CompanyId,
                DealId = request.DealId,
                SortBy = request.SortBy ?? nameof(request.Id),
                SortDirection = request.SortDirection ?? DefaultValues.SORTDIRECTION,
                PageNumber = request.PageNumber ?? DefaultValues.PAGENUMBER,
                PageSize = request.PageSize ?? DefaultValues.PAGESIZE,
            };
        }

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

        internal static UpdateMeetingDTO MapToDto(this UpdateMeetingRequest request, Guid id, Guid userId)
        {
            return new()
            {
                Id = id,
                UserId = userId,
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
