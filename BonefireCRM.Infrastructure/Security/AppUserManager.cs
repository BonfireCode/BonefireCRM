using BonefireCRM.Domain.DTOs.Security;
using BonefireCRM.Domain.Infrastructure.Email;
using BonefireCRM.Domain.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Text;

namespace BonefireCRM.Infrastructure.Security
{
    internal class AppUserManager : IAppUserManager
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailSender _emailSender;

        public AppUserManager(UserManager<ApplicationUser> userManager, IEmailSender emailSender)
        {
            _userManager = userManager;
            _emailSender = emailSender;
        }

        public async Task<RegisterResultDTO> CreateAsync(RegisterDTO registerDTO)
        {
            var registerResultDTO = new RegisterResultDTO();

            var emailAddressAttribute = new EmailAddressAttribute();
            if (string.IsNullOrEmpty(registerDTO.Email) || !emailAddressAttribute.IsValid(registerDTO.Email))
            {
                registerResultDTO.ValidationError = new("InvalidEmail", "The provided email is invalid");
                return registerResultDTO;
            }

            var user = new ApplicationUser
            {
                UserName = registerDTO.UserName,
                Email = registerDTO.Email,
            };

            var identityResult = await _userManager.CreateAsync(user, registerDTO.Password);
            if (!identityResult.Succeeded)
            {
                registerResultDTO.ValidationError = new("UserCreationFailed", identityResult.Errors.First().Description);
                return registerResultDTO;
            }

            registerResultDTO.UserId = await _userManager.GetUserIdAsync(user);

            var confirmationtoken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            await _emailSender.SendConfirmationEmailAsync(registerResultDTO.UserId, user.Email!, confirmationtoken);

            return registerResultDTO;
        }

        public async Task<bool> ConfirmEmailAsync(ConfirmEmailDTO confirmEmailDTO)
        {
            var user = await _userManager.FindByIdAsync(confirmEmailDTO.UserId);
            if (user is null)
            {
                return false;
            }

            string token;
            try
            {
                token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(confirmEmailDTO.Token));
            }
            catch (FormatException)
            {
                return false;
            }

            var result = string.IsNullOrWhiteSpace(confirmEmailDTO.ChangedEmail)
                ? await _userManager.ConfirmEmailAsync(user, token)
                : await _userManager.ChangeEmailAsync(user, confirmEmailDTO.ChangedEmail, token);

            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task ResendConfirmationEmail(ResendConfirmationDTO resendConfirmationDTO)
        {
            var user = await _userManager.FindByEmailAsync(resendConfirmationDTO.Email);

            if (user is not null)
            {
                var confirmationtoken = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                await _emailSender.SendConfirmationEmailAsync(user.Id, user.Email!, confirmationtoken);
            }
        }

        public async Task ForgotPassword(ForgotPasswordDTO forgotPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(forgotPasswordDTO.Email);

            if (user is not null && await _userManager.IsEmailConfirmedAsync(user))
            {
                var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
                await _emailSender.SendPasswordResetEmailAsync(resetToken, user.Email!);
            }
        }

        public async Task<bool> ResetPassword(ResetPasswordDTO resetPasswordDTO)
        {
            var user = await _userManager.FindByEmailAsync(resetPasswordDTO.Email);
            if (user is null || !(await _userManager.IsEmailConfirmedAsync(user)))
            {
                return false;
            }

            IdentityResult result;
            try
            {
                var code = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(resetPasswordDTO.ResetCode));
                result = await _userManager.ResetPasswordAsync(user, code, resetPasswordDTO.NewPassword);
            }
            catch (FormatException)
            {
                return false;
            }

            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

        public async Task<KeyAndRecoveryCodesDTO> GetKeyAndRecoveryCodes(TwoFactorDTO twoFactorDTO, ClaimsPrincipal claimsPrincipal)
        {
            var keyAndRecoveryCodesDTO = new KeyAndRecoveryCodesDTO();

            if (await _userManager.GetUserAsync(claimsPrincipal) is not { } user)
            {
                keyAndRecoveryCodesDTO.ValidationError = new("UserNotFound", "No user was found");
                return keyAndRecoveryCodesDTO;
            }

            if (twoFactorDTO.Enable == true)
            {
                if (twoFactorDTO.ResetSharedKey)
                {
                    keyAndRecoveryCodesDTO.ValidationError = new("CannotResetSharedKeyAndEnable",
                        "Resetting the 2fa shared key must disable 2fa until a 2fa token based on the new shared key is validated.");
                    return keyAndRecoveryCodesDTO;
                }

                if (string.IsNullOrEmpty(twoFactorDTO.TwoFactorCode))
                {
                    keyAndRecoveryCodesDTO.ValidationError = new("RequiresTwoFactor",
                        "No 2fa token was provided by the request. A valid 2fa token is required to enable 2fa.");
                    return keyAndRecoveryCodesDTO;
                }

                if (!await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider, twoFactorDTO.TwoFactorCode))
                {
                    keyAndRecoveryCodesDTO.ValidationError = new("InvalidTwoFactorCode",
                        "The 2fa token provided by the request was invalid. A valid 2fa token is required to enable 2fa.");
                    return keyAndRecoveryCodesDTO;
                }

                await _userManager.SetTwoFactorEnabledAsync(user, true);
            }
            else if (twoFactorDTO.Enable == false || twoFactorDTO.ResetSharedKey)
            {
                await _userManager.SetTwoFactorEnabledAsync(user, false);
            }

            if (twoFactorDTO.ResetSharedKey)
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
            }

            if (twoFactorDTO.ResetRecoveryCodes || (twoFactorDTO.Enable == true && await _userManager.CountRecoveryCodesAsync(user) == 0))
            {
                var recoveryCodesEnumerable = await _userManager.GenerateNewTwoFactorRecoveryCodesAsync(user, 10);
                keyAndRecoveryCodesDTO.RecoveryCodes = recoveryCodesEnumerable?.ToArray() ?? [];
            }

            var key = await _userManager.GetAuthenticatorKeyAsync(user);
            if (string.IsNullOrEmpty(key))
            {
                await _userManager.ResetAuthenticatorKeyAsync(user);
                key = await _userManager.GetAuthenticatorKeyAsync(user);

                if (string.IsNullOrEmpty(key))
                {
                    throw new NotSupportedException("The user manager must produce an authenticator key after reset.");
                }
            }
            keyAndRecoveryCodesDTO.Key = key;

            keyAndRecoveryCodesDTO.RecoveryCodesLeft = keyAndRecoveryCodesDTO.RecoveryCodes.Length == 0
                ? await _userManager.CountRecoveryCodesAsync(user)
                : keyAndRecoveryCodesDTO.RecoveryCodes.Length;

            keyAndRecoveryCodesDTO.IsTwoFactorEnabled = await _userManager.GetTwoFactorEnabledAsync(user);

            return keyAndRecoveryCodesDTO;
        }

        public async Task<GetInfoResultDTO> GetInfo(ClaimsPrincipal claimsPrincipal)
        {
            var getinfoDTO = new GetInfoResultDTO();
            if (await _userManager.GetUserAsync(claimsPrincipal) is not { } user)
            {
                getinfoDTO.ValidationError = new("UserNotFound", "No user was found");
                return getinfoDTO;
            }

            getinfoDTO.Email = await _userManager.GetEmailAsync(user) ?? throw new NotSupportedException("Users must have an email.");
            getinfoDTO.IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return getinfoDTO;
        }

        public async Task<CreateInfoResultDTO> CreateInfo(CreateInfoDTO createInfoDTO, ClaimsPrincipal claimsPrincipal)
        {
            var createInfoResultDTO = new CreateInfoResultDTO();
            if (await _userManager.GetUserAsync(claimsPrincipal) is not { } user)
            {
                createInfoResultDTO.ValidationError = new("UserNotFound", "No user was found");
                return createInfoResultDTO;
            }

            var emailAddressAttribute = new EmailAddressAttribute();
            if (!string.IsNullOrEmpty(createInfoDTO.NewEmail) && !emailAddressAttribute.IsValid(createInfoDTO.NewEmail))
            {
                createInfoResultDTO.ValidationError = new("InvalidEmail", "The provided email is invalid");
                return createInfoResultDTO;
            }

            if (!string.IsNullOrEmpty(createInfoDTO.NewPassword))
            {
                if (string.IsNullOrEmpty(createInfoDTO.OldPassword))
                {
                    createInfoResultDTO.ValidationError = new("OldPasswordRequired", "The old password is required to set a new password. If the old password is forgotten, use /resetPassword.");
                    return createInfoResultDTO;
                }

                var changePasswordResult = await _userManager.ChangePasswordAsync(user, createInfoDTO.OldPassword, createInfoDTO.NewPassword);
                if (!changePasswordResult.Succeeded)
                {
                    createInfoResultDTO.ValidationError = new("ChangePasswordFailed", changePasswordResult.Errors.First().Description);
                    return createInfoResultDTO;
                }
            }

            if (!string.IsNullOrEmpty(createInfoDTO.NewEmail))
            {
                var email = await _userManager.GetEmailAsync(user);

                if (email != createInfoDTO.NewEmail)
                {
                    var changeToken = await _userManager.GenerateChangeEmailTokenAsync(user, createInfoDTO.NewEmail);
                    await _emailSender.SendChangeEmailAsync(user.Id, createInfoDTO.NewEmail, changeToken);
                }
            }

            createInfoResultDTO.Email = await _userManager.GetEmailAsync(user) ?? throw new NotSupportedException("Users must have an email.");
            createInfoResultDTO.IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return createInfoResultDTO;
        }

        public async Task<string> GenerateTwoFactorCodeAsync(ClaimsPrincipal claimsPrincipal)
        {
            var user = await _userManager.GetUserAsync(claimsPrincipal);
            var code = await _userManager.GenerateTwoFactorTokenAsync(user!, TokenOptions.DefaultEmailProvider);
            return code;
        }

        public async Task<bool> DeleteUserAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var result = await _userManager.DeleteAsync(user!);

            return result.Succeeded;
        }
    }
}
