// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.User;
using BonefireCRM.Domain.DTOs.User;

namespace BonefireCRM.API.User.Mappers
{
    internal static class DtoToResponseMapper
    {
        internal static GetUserResponse MapToResponse(this GetUserDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };
        }

        internal static UpdateUserResponse MapToResponse(this UpdatedUserDTO dto)
        {
            return new()
            {
                Id = dto.Id,
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
            };
        }
    }
}
