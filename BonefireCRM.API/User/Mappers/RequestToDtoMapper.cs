// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.User;
using BonefireCRM.Domain.Constants;
using BonefireCRM.Domain.DTOs.User;

namespace BonefireCRM.API.User.Mappers
{
    internal static class RequestToDtoMapper
    {
        internal static GetAllUsersDTO MapToDto(this GetUsersRequest request)
        {
            return new()
            {
                Id = request.Id,
                RegisterId = request.RegisterId,
                UserName = request.UserName,
                Email = request.Email,
                FirstName = request.FirstName,
                LastName = request.LastName,
                SortBy = request.SortBy ?? nameof(request.Id),
                SortDirection = request.SortDirection ?? DefaultValues.SORTDIRECTION,
                PageNumber = request.PageNumber ?? DefaultValues.PAGENUMBER,
                PageSize = request.PageSize ?? DefaultValues.PAGESIZE,
            };
        }

        internal static UpdateUserDTO MapToDto(this UpdateUserRequest request, Guid id, Guid registerId)
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
