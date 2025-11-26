// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Contact;
using BonefireCRM.Domain.Constants;
using BonefireCRM.Domain.DTOs.Contact;

namespace BonefireCRM.API.Contact.Mappers
{
    internal static class RequestToDtoMapper
    {
        internal static GetAllContactsDTO MapToDto(this GetContactsRequest request)
        {
            return new()
            {
                Id = request.Id,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                JobRole = request.JobRole,
                PhoneNumber = request.PhoneNumber,
                UserId = request.UserId,
                LifecycleStageId = request.LifecycleStageId,
                CompanyId = request.CompanyId,
                SortBy = request.SortBy ?? nameof(request.Id),
                SortDirection = request.SortDirection ?? DefaultValues.SORTDIRECTION,
                PageNumber = request.PageNumber ?? DefaultValues.PAGENUMBER,
                PageSize = request.PageSize ?? DefaultValues.PAGESIZE,
            };
        }

        internal static CreateContactDTO MapToDto(this CreateContactRequest request, Guid userId)
        {
            return new()
            {
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                JobRole = request.JobRole,
                PhoneNumber = request.PhoneNumber,
                LifecycleStageId = request.LifecycleStageId,
                CompanyId = request.CompanyId,
                UserId = userId,
            };
        }

        internal static UpdateContactDTO MapToDto(this UpdateContactRequest request, Guid id,  Guid userId)
        {
            return new()
            {
                Id = id,
                UserId = userId,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                JobRole = request.JobRole,
                PhoneNumber = request.PhoneNumber,
                LifecycleStageId = request.LifecycleStageId,
                CompanyId = request.CompanyId,
            };
        }
    }
}
