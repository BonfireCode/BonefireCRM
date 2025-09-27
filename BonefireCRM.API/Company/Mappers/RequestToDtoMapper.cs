// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Company;
using BonefireCRM.Domain.DTOs.Company;

namespace BonefireCRM.API.Company.Mappers
{
    internal static class RequestToDtoMapper
    {
        internal static CreateCompanyDTO MapToDto(CreateCompanyRequest request)
        {
            return new()
            {
                Name = request.Name,
                Industry = request.Industry,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
            };
        }

        internal static UpdateCompanyDTO MapToDto(UpdateCompanyRequest request)
        {
            return new()
            {
                Name = request.Name,
                Industry = request.Industry,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
            };
        }
    }
}
