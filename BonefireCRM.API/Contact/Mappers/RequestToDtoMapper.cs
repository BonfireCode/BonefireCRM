// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Contact;
using BonefireCRM.Domain.DTOs.Contact;

namespace BonefireCRM.API.Contact.Mappers
{
    internal static class RequestToDtoMapper
    {
        internal static GetContactsDTO MapToDto(this GetContactsRequest request)
        {
            return new()
            {
                PageNumber = request.PageNumber,
                PageSize = request.PageSize,
                SortBy = request.SortBy,
                SortDirection = request.SortDirection,
                Id = request.Id,
                FirstName = request.FirstName,
                LastName = request.LastName,
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

        internal static UpdateContactDTO MapToDto(this UpdateContactRequest request, Guid id)
        {
            return new()
            {
                Id = id,
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
