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
        private readonly IBaseRepository<DealParticipantRole> _dealParticipantRoleRepository;

        public SecurityService(IAppUserManager appUserManager, IAppSignInManager appSignInManager, IBaseRepository<User> userRepository, IBaseRepository<DealParticipantRole> dealParticipantRoleRepository)
        {
            _appUserManager = appUserManager;
            _appSignInManager = appSignInManager;
            _userRepository = userRepository;
            _dealParticipantRoleRepository = dealParticipantRoleRepository;
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
            await SeedDealParticipantRoles(user, ct);

            if (createdUser is null)
            {
                return Fin<RegisterResultDTO>.Fail(new AddEntityException<User>());
            }

            return registerResultDTO;
        }

        private async Task SeedDealParticipantRoles(User user, CancellationToken ct)
        {
            var defaultRoles = new List<DealParticipantRole>
            {
                new() { Name = "Decision Maker", Description = "Responsible for final decision-making in the deal" },
                new() { Name = "Influencer", Description = "Influences the decision but does not have final authority" },
                new() { Name = "Evaluator", Description = "Evaluates the proposal and provides feedback" },
                new() { Name = "End User", Description = "Will ultimately use the product or service" }
            };

            foreach (var role in defaultRoles)
            {
                role.RegisteredByUserId = user.Id;

                await _dealParticipantRoleRepository.AddAsync(role, ct);
            }
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
