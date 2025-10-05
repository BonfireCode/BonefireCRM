// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.User;
using BonefireCRM.Domain.DTOs.User;

namespace BonefireCRM.API.User.Mappers
{
    internal static class RequestToDtoMapper
    {
        internal static UpdateUserDTO MapToDto(UpdateUserRequest request, Guid id, Guid registerId)
        {
            return new()
            {
                Id = id,
                RegisterId = registerId,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
            };
        }
    }
}
