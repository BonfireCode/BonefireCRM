// <copyright file="RequestToDtoMapper.cs" company="Bonefire">
// Copyright (c) Bonefire. All rights reserved.
// </copyright>

using BonefireCRM.API.Contrat.Security;
using BonefireCRM.Domain.DTOs.Security;

namespace BonefireCRM.API.Security.Mappers
{
    internal static class RequestToDtoMapper
    {
        internal static RegisterDTO MapToDto(this RegisterRequest request)
        {
            return new()
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
            };
        }

        internal static LoginDTO MapToDto(this LoginRequest request, bool useCookies, bool useSessionCookies)
        {
            return new()
            {
                UserName = request.UserName,
                Password = request.Password,
                TwoFactorCode = request.TwoFactorCode,
                TwoFactorRecoveryCode = request.TwoFactorRecoveryCode,
                UseCookies = useCookies,
                UseSessionCookies = useSessionCookies,
            };
        }

        internal static RefreshDTO MapToDto(this RefreshRequest request)
        {
            return new()
            {
                RefreshToken = request.RefreshToken,
            };
        }

        internal static ConfirmEmailDTO MapToDto(this ConfirmEmailRequest request)
        {
            return new()
            {
                UserId = request.UserId,
                Token = request.Token,
                ChangedEmail = request.ChangedEmail,
            };
        }

        internal static ResendConfirmationDTO MapToDto(this ResendConfirmationRequest request)
        {
            return new()
            {
                Email = request.Email,
            };
        }

        internal static ForgotPasswordDTO MapToDto(this ForgotPasswordRequest request)
        {
            return new()
            {
                Email = request.Email,
            };
        }

        internal static ResetPasswordDTO MapToDto(this ResetPasswordRequest request)
        {
            return new()
            {
                Email = request.Email,
                ResetCode = request.ResetCode,
                NewPassword = request.NewPassword,
            };
        }

        internal static TwoFactorDTO MapToDto(this TwoFactorRequest request)
        {
            return new()
            {
                Enable = request.Enable,
                ForgetMachine = request.ForgetMachine,
                ResetRecoveryCodes = request.ResetRecoveryCodes,
                ResetSharedKey = request.ResetSharedKey,
                TwoFactorCode = request.TwoFactorCode,
            };
        }

        internal static CreateInfoDTO MapToDto(this CreateInfoRequest request)
        {
            return new()
            {
                NewEmail = request.NewEmail,
                NewPassword = request.NewPassword,
                OldPassword = request.OldPassword,
            };
        }
    }
}
