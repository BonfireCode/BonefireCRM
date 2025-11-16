// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.DealParticipantRole;
using BonefireCRM.Domain.DTOs.DealParticipantRole;

namespace BonefireCRM.API.DealParticipantRole.Mappers
{
    internal static class DtoToResponseMapper
    {
        internal static GetDealParticipantRoleResponse MapToResponse(this GetDealParticipantRoleDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Name = dto.Name,
                Description = dto.Description,
            };
        }
    }
}
