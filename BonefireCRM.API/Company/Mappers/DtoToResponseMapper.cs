// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Company;
using BonefireCRM.Domain.DTOs.Company;

namespace BonefireCRM.API.Company.Mappers
{
    internal static class DtoToResponseMapper
    {
        internal static GetCompanyResponse MapToResponse(this GetCompanyDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Industry = dto.Industry,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
            };
        }

        internal static CreateCompanyResponse MapToResponse(this CreatedCompanyDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Industry = dto.Industry,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
            };
        }

        internal static UpdateCompanyResponse MapToResponse(this UpdatedCompanyDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Industry = dto.Industry,
                Address = dto.Address,
                PhoneNumber = dto.PhoneNumber,
            };
        }
    }
}
