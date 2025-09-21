using BonefireCRM.Domain.DTOs.Security;
using System.Security.Claims;

namespace BonefireCRM.Domain.Infrastructure.Security
{
    public interface IAppUserManager
    {
        Task<RegisterResultDTO> CreateAsync(RegisterDTO dtoRegister);
        Task<bool> ConfirmEmailAsync(ConfirmEmailDTO confirmEmailDTO);
        Task ResendConfirmationEmail(ResendConfirmationDTO resendConfirmationDTO);
        Task ForgotPassword(ForgotPasswordDTO forgotPasswordDTO);
        Task<bool> ResetPassword(ResetPasswordDTO resetPasswordDTO);
        Task<KeyAndRecoveryCodesDTO> GetKeyAndRecoveryCodes(TwoFactorDTO twoFactorDTO, ClaimsPrincipal claimsPrincipal);
        Task<GetInfoResultDTO> GetInfo(ClaimsPrincipal claimsPrincipal);
        Task<CreateInfoResultDTO> CreateInfo(CreateInfoDTO createInfoDTO, ClaimsPrincipal claimsPrincipal);
        Task<string> GenerateTwoFactorCodeAsync(ClaimsPrincipal claimsPrincipal);
    }
}
