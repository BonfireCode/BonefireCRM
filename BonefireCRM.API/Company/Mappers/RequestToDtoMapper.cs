// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Company;
using BonefireCRM.Domain.Constants;
using BonefireCRM.Domain.DTOs.Company;

namespace BonefireCRM.API.Company.Mappers
{
    internal static class RequestToDtoMapper
    {
        internal static GetAllCompaniesDTO MapToDto(this GetCompaniesRequest request)
        {
            return new()
            {
                Id = request.Id,
                Name = request.Name,
                Industry = request.Industry,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
                SortBy = request.SortBy ?? nameof(request.Id),
                SortDirection = request.SortDirection ?? DefaultValues.SORTDIRECTION,
                PageNumber = request.PageNumber ?? DefaultValues.PAGENUMBER,
                PageSize = request.PageSize ?? DefaultValues.PAGESIZE,
            };
        }

        internal static CreateCompanyDTO MapToDto(this CreateCompanyRequest request)
        {
            return new()
            {
                Name = request.Name,
                Industry = request.Industry,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
            };
        }

        internal static UpdateCompanyDTO MapToDto(this UpdateCompanyRequest request, Guid id)
        {
            return new()
            {
                Id = id,
                Name = request.Name,
                Industry = request.Industry,
                Address = request.Address,
                PhoneNumber = request.PhoneNumber,
            };
        }
    }
}
