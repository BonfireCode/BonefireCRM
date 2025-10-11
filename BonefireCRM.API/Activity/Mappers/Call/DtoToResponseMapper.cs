// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Call;
using BonefireCRM.Domain.DTOs.Activity.Call;

namespace BonefireCRM.API.Activity.Mappers.Call
{
    internal static class DtoToResponseMapper
    {
        internal static GetCallResponse MapToResponse(this GetCallDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                CompanyId = dto.CompanyId,
                ContactId = dto.ContactId,
                CallTime = dto.CallTime,
                Duration = dto.Duration,
                Notes = dto.Notes,
                DealId = dto.DealId,
            };
        }

        internal static CreateCallResponse MapToResponse(this CreatedCallDTO dto)
        {
            return new()
            {
                DealId = dto.DealId,
                Id = dto.Id,
                ContactId = dto.ContactId,
                CompanyId = dto.CompanyId,
                CallTime = dto.CallTime,
                Duration = dto.Duration,
                Notes = dto.Notes,
            };
        }

        internal static UpdateCallResponse MapToResponse(this UpdatedCallDTO dto)
        {
            return new()
            {
                Notes = dto.Notes,
                Id = dto.Id,
                ContactId = dto.ContactId,
                CompanyId = dto.CompanyId,
                CallTime = dto.CallTime,
                Duration = dto.Duration,
                DealId = dto.DealId,
            };
        }
    }
}
