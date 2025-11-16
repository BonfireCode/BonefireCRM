// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.DealParticipantRole;
using BonefireCRM.Domain.Constants;
using BonefireCRM.Domain.DTOs.DealParticipantRole;

namespace BonefireCRM.API.DealParticipantRole.Mappers
{
    internal static class RequestToDtoMapper
    {
        internal static GetAllDealParticipantRolesDTO MapToDto(this GetDealParticipantRolesRequest request, Guid userId)
        {
            return new()
            {
                Id = request.Id,
                RegisteredByUserId = userId,
                Name = request.Name,
                Description = request.Description,
                SortBy = request.SortBy ?? nameof(request.Id),
                SortDirection = request.SortDirection ?? DefaultValues.SORTDIRECTION,
                PageNumber = request.PageNumber ?? DefaultValues.PAGENUMBER,
                PageSize = request.PageSize ?? DefaultValues.PAGESIZE,
            };
        }
    }
}
