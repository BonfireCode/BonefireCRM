// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Call;
using BonefireCRM.API.Contrat.Company;
using BonefireCRM.Domain.DTOs.Activity.Call;
using BonefireCRM.Domain.DTOs.Company;

namespace BonefireCRM.API.Company.Mappers.Call
{
    internal static class RequestToDtoMapper
    {
        internal static CreateCallDTO MapToDto(CreateCallRequest request)
        {
            return new()
            {
            };
        }

        internal static UpdateCallDTO MapToDto(UpdateCallRequest request, Guid id)
        {
            return new()
            {
            };
        }
    }
}
