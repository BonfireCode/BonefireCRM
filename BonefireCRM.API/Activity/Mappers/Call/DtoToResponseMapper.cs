// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Call;
using BonefireCRM.Domain.DTOs.Activity.Call;

namespace BonefireCRM.API.Company.Mappers.Call
{
    internal static class DtoToResponseMapper
    {
        internal static GetCallResponse MapToResponse(this GetCallDTO dto)
        {
            return new()
            {
            };
        }

        internal static CreateCallResponse MapToResponse(this CreatedCallDTO dto)
        {
            return new()
            {
            };
        }

        internal static UpdateCallResponse MapToResponse(this UpdatedCallDTO dto)
        {
            return new()
            {
            };
        }
    }
}
