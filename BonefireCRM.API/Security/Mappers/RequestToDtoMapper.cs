using BonefireCRM.API.Contrat.Security;
using BonefireCRM.Domain.DTOs.Security;

namespace BonefireCRM.API.Security.Mappers
{
    internal static class RequestToDtoMapper
    {
        internal static RegisterDTO MapToDto(RegisterRequest request)
        {
            return new()
            {
                UserName = request.UserName,
                Email = request.Email,
                Password = request.Password,
            };
        }

        internal static LoginDTO MapToDto(LoginRequest request, bool useCookies, bool useSessionCookies)
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

        internal static RefreshDTO MapToDto(RefreshRequest request)
        {
            return new()
            {
                RefreshToken = request.RefreshToken,
            };
        }

        internal static ConfirmEmailDTO MapToDto(ConfirmEmailRequest request)
        {
            return new()
            {
                UserId = request.UserId,
                Token = request.Token,
                ChangedEmail = request.ChangedEmail,
            };
        }

        internal static ResendConfirmationDTO MapToDto(ResendConfirmationRequest request)
        {
            return new()
            {
                Email = request.Email,
            };
        }

        internal static ForgotPasswordDTO MapToDto(ForgotPasswordRequest request)
        {
            return new()
            {
                Email = request.Email,
            };
        }

        internal static ResetPasswordDTO MapToDto(ResetPasswordRequest request)
        {
            return new()
            {
                Email = request.Email,
                ResetCode = request.ResetCode,
                NewPassword = request.NewPassword,
            };
        }

        internal static TwoFactorDTO MapToDto(TwoFactorRequest request)
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

        internal static CreateInfoDTO MapToDto(CreateInfoRequest request)
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
