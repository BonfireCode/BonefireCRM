// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Call;
using BonefireCRM.Domain.DTOs.Activity.Call;

namespace BonefireCRM.API.Company.Mappers.Call
{
    internal static class RequestToDtoMapper
    {
        internal static CreateCallDTO MapToDto(CreateCallRequest request, Guid userId)
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

        internal static UpdateCallDTO MapToDto(UpdateCallRequest request, Guid id)
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
            };
        }
    }
}
