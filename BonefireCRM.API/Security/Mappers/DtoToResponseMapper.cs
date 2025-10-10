// <copyright file="DtoToResponseMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Security;
using BonefireCRM.Domain.DTOs.Security;

namespace BonefireCRM.API.Security.Mappers
{
    internal static class DtoToResponseMapper
    {
        internal static TwoFactorResponse MapToResponse(this TwoFactorResultDTO dto)
        {
            return new()
            {
                SharedKey = dto.SharedKey,
                RecoveryCodesLeft = dto.RecoveryCodesLeft,
                RecoveryCodes = dto.RecoveryCodes,
                IsMachineRemembered = dto.IsMachineRemembered,
                IsTwoFactorEnabled = dto.IsTwoFactorEnabled,
            };
        }

        internal static GetInfoResponse? MapToResponse(this GetInfoResultDTO dto)
        {
            return new()
            {
                UserId = dto.UserId,
                Email = dto.Email,
                IsEmailConfirmed = dto.IsEmailConfirmed,
            };
        }

        internal static CreateInfoResponse MapToResponse(this CreateInfoResultDTO dto)
        {
            return new()
            {
                Email = dto.Email,
                IsEmailConfirmed = dto.IsEmailConfirmed,
            };
        }
    }
}
