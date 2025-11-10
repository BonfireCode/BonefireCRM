// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Contact;
using BonefireCRM.API.Contrat.Shared;
using BonefireCRM.Domain.DTOs.Contact;
using BonefireCRM.Domain.DTOs.Shared;

namespace BonefireCRM.API.Contact.Mappers
{
    internal static class RequestToDtoMapper
    {

        internal static FilterRequestDTO MapToDto(this FilterRequest request)
        {
            return new FilterRequestDTO
            {
                Page = request.Page,
                PageSize = request.PageSize,

                Sort = request.Sort?
                .Select(s => new Domain.DTOs.Shared.SortDefinition
                {
                    Field = s.Field,
                    Direction = s.Direction,
                })
                .ToList(),
                Filter = request.Filter != null ? MapFilterGroup(request.Filter) : null,
            };
        }

        private static Domain.DTOs.Shared.FilterGroup MapFilterGroup(Contrat.Shared.FilterGroup group)
        {
            return new Domain.DTOs.Shared.FilterGroup
            {
                Logic = group.Logic ?? "AND",

                Filters = group.Filters?
                    .Select(f => new Domain.DTOs.Shared.Filter
                    {
                        Field = f.Field,
                        Operator = f.Operator,
                        Value = f.Value,
                    })
                    .ToList(),

                Groups = group.Groups?
                    .Select(MapFilterGroup)
                    .ToList(),
            };
        }

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
