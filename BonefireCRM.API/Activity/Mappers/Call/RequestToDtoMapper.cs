// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Call;
using BonefireCRM.Domain.Constants;
using BonefireCRM.Domain.DTOs.Activity.Call;

namespace BonefireCRM.API.Activity.Mappers.Call
{
    internal static class RequestToDtoMapper
    {
        internal static GetAllCallsDTO MapToDto(this GetCallsRequest request)
        {
            return new()
            {
                Id = request.Id,
                CallTime = request.CallTime,
                Duration = request.Duration,
                Notes = request.Notes,
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

        internal static CreateCallDTO MapToDto(this CreateCallRequest request, Guid userId)
        {
            return new()
            {
                DealId = request.DealId,
                ContactId = request.ContactId,
                CompanyId = request.CompanyId,
                CallTime = request.CallTime,
                Duration = request.Duration,
                Notes = request.Notes,
                UserId = userId,
            };
        }

        internal static UpdateCallDTO MapToDto(this UpdateCallRequest request, Guid id, Guid userId)
        {
            return new()
            {
                Id = id,
                DealId = request.DealId,
                ContactId = request.ContactId,
                CompanyId = request.CompanyId,
                CallTime = request.CallTime,
                Duration = request.Duration,
                Notes = request.Notes,
                UserId = userId,
            };
        }
    }
}
