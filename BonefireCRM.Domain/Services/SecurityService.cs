using BonefireCRM.Domain.DTOs.Security;
using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Exceptions;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Infrastructure.Security;
using BonefireCRM.Domain.Mappers;
using LanguageExt;
using System.Security.Claims;

namespace BonefireCRM.Domain.Services
{
    public class SecurityService
    {
        private readonly IAppUserManager _appUserManager;
        private readonly IAppSignInManager _appSignInManager;
        private readonly IBaseRepository<User> _userRepository;

        public SecurityService(IAppUserManager appUserManager, IAppSignInManager appSignInManager, IBaseRepository<User> userRepository)
        {
            _appUserManager = appUserManager;
            _appSignInManager = appSignInManager;
            _userRepository = userRepository;
        }

        public async Task<Fin<RegisterResultDTO>> RegisterUser(RegisterDTO registerDTO, CancellationToken ct)
        {
            var registerResultDTO = await _appUserManager.CreateAsync(registerDTO);
            if (!string.IsNullOrEmpty(registerResultDTO.ValidationError.Key))
            {
                return Fin<RegisterResultDTO>.Fail(new RegisterUserException(registerResultDTO.ValidationError.Key, registerResultDTO.ValidationError.Value));
            }

            var user = registerDTO.MapToUser(registerResultDTO.UserId);
            var createdUser = await _userRepository.AddAsync(user, ct);
            if (createdUser is null)
            {
                return Fin<RegisterResultDTO>.Fail(new AddEntityException<User>());
            }

            return registerResultDTO;
        }

        public async Task<LoginResultDTO> LoginUser(LoginDTO loginDTO, CancellationToken ct)
        {
            var loginResultDTO = await _appSignInManager.PasswordSignInAsync(loginDTO);

            return loginResultDTO;
        }

        public async Task<RefreshResultDTO> RefreshUserToken(RefreshDTO refreshDTO, CancellationToken ct)
        {
            var refreshResultDTO = await _appSignInManager.RefreshUserToken(refreshDTO);

            return refreshResultDTO;
        }

        public async Task<Fin<bool>> ConfirmUserEmail(ConfirmEmailDTO confirmEmailDTO, CancellationToken ct)
        {
            var isEmailConfirmed = await _appUserManager.ConfirmEmailAsync(confirmEmailDTO);
            if (!isEmailConfirmed)
            {
                return Fin<bool>.Fail(new UnauthorisedException());
            }

            return isEmailConfirmed;
        }

        public async Task ResendUserConfirmation(ResendConfirmationDTO resendConfirmationDTO, CancellationToken ct)
        {
            await _appUserManager.ResendConfirmationEmail(resendConfirmationDTO);
        }

        public async Task ForgotUserPassword(ForgotPasswordDTO forgotPasswordDTO, CancellationToken ct)
        {
            await _appUserManager.ForgotPassword(forgotPasswordDTO);
        }

        public async Task<Fin<bool>> ResetUserPassword(ResetPasswordDTO resetPasswordDTO, CancellationToken ct)
        {
            var isPasswordReset = await _appUserManager.ResetPassword(resetPasswordDTO);
            if (!isPasswordReset) 
            {
                return Fin<bool>.Fail(new ResetPasswordException());
            }

            return isPasswordReset;
        }

        public async Task<Fin<TwoFactorResultDTO>> ManageUserTwoFactor(TwoFactorDTO twoFactorDTO, ClaimsPrincipal claimsPrincipal, CancellationToken ct)
        {
            var keyAndRecoveryCodesDTO = await _appUserManager.GetKeyAndRecoveryCodes(twoFactorDTO, claimsPrincipal);

            if (!string.IsNullOrEmpty(keyAndRecoveryCodesDTO.ValidationError.Key))
            {
                return Fin<TwoFactorResultDTO>.Fail(new TwoFactorException(keyAndRecoveryCodesDTO.ValidationError.Key, keyAndRecoveryCodesDTO.ValidationError.Value));
            }
            
            var isMachineRemembered = await _appSignInManager.RememberMachine(twoFactorDTO, claimsPrincipal);

            var twoFactorResultDTO = new TwoFactorResultDTO
            {
                SharedKey = keyAndRecoveryCodesDTO.Key,
                RecoveryCodes = keyAndRecoveryCodesDTO.RecoveryCodes,
                RecoveryCodesLeft = keyAndRecoveryCodesDTO.RecoveryCodesLeft,
                IsTwoFactorEnabled = keyAndRecoveryCodesDTO.IsTwoFactorEnabled,
                IsMachineRemembered = isMachineRemembered,
            };

            return twoFactorResultDTO;
        }

        public async Task<Fin<GetInfoResultDTO>> ManageGetUserInfo(ClaimsPrincipal claimsPrincipal, CancellationToken ct)
        {
            var getInfoResultDTO = await _appUserManager.GetInfo(claimsPrincipal);

            if (!string.IsNullOrEmpty(getInfoResultDTO.ValidationError.Key))
            {
                return Fin<GetInfoResultDTO>.Fail(new GetInfoException(getInfoResultDTO.ValidationError.Key, getInfoResultDTO.ValidationError.Value));
            }

            return getInfoResultDTO;
        }

        public async Task<Fin<CreateInfoResultDTO>> ManageCreateUserInfo(CreateInfoDTO createInfoDTO, ClaimsPrincipal claimsPrincipal, CancellationToken ct)
        {
            var createInfoResultDTO = await _appUserManager.CreateInfo(createInfoDTO, claimsPrincipal);

            if (!string.IsNullOrEmpty(createInfoResultDTO.ValidationError.Key))
            {
                return Fin<CreateInfoResultDTO>.Fail(new CreateInfoException(createInfoResultDTO.ValidationError.Key, createInfoResultDTO.ValidationError.Value));
            }

            return createInfoResultDTO;
        }

        public async Task LogoutUser(CancellationToken ct)
        {
            await _appSignInManager.SignOutAsync();
        }

        public async Task<string> GenerateTwoFactorCode(ClaimsPrincipal claimsPrincipal, CancellationToken ct)
        {
            var code = await _appUserManager.GenerateTwoFactorCodeAsync(claimsPrincipal);
            return code;
        }
    }
}
