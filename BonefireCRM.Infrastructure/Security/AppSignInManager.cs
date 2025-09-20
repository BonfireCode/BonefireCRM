using BonefireCRM.Domain.DTOs.Security;
using BonefireCRM.Domain.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Security.Claims;

namespace BonefireCRM.Infrastructure.Security
{
    internal class AppSignInManager : IAppSignInManager
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IOptionsMonitor<BearerTokenOptions> _bearerTokenOptions;
        private readonly TimeProvider _timeProvider;
        private readonly UserManager<ApplicationUser> _userManager;

        public AppSignInManager(SignInManager<ApplicationUser> signInManager, IOptionsMonitor<BearerTokenOptions> bearerTokenOptions, TimeProvider timeProvider, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _bearerTokenOptions = bearerTokenOptions;
            _timeProvider = timeProvider;
            _userManager = userManager;
        }

        public async Task<LoginResultDTO> PasswordSignInAsync(LoginDTO loginDTO)
        {
            var useCookieScheme = (loginDTO.UseCookies == true) || (loginDTO.UseSessionCookies == true);
            var isPersistent = (loginDTO.UseCookies == true) && (loginDTO.UseSessionCookies != true);
            _signInManager.AuthenticationScheme = useCookieScheme ? IdentityConstants.ApplicationScheme : IdentityConstants.BearerScheme;

            var signInResult = await _signInManager.PasswordSignInAsync(loginDTO.UserName , loginDTO.Password, isPersistent, lockoutOnFailure: true);

            if (signInResult.RequiresTwoFactor)
            {
                if (!string.IsNullOrEmpty(loginDTO.TwoFactorCode))
                {
                    signInResult = await _signInManager.TwoFactorAuthenticatorSignInAsync(loginDTO.TwoFactorCode, isPersistent, rememberClient: isPersistent);
                }
                else if (!string.IsNullOrEmpty(loginDTO.TwoFactorRecoveryCode))
                {
                    signInResult = await _signInManager.TwoFactorRecoveryCodeSignInAsync(loginDTO.TwoFactorRecoveryCode);
                }
            }

            var result = new LoginResultDTO
            {
                Succeeded = signInResult.Succeeded,
                State = signInResult.ToString(),
            };

            return result;
        }

        public async Task<RefreshResultDTO> RefreshUserToken(RefreshDTO refreshDTO)
        {
            var refreshTokenProtector = _bearerTokenOptions.Get(IdentityConstants.BearerScheme).RefreshTokenProtector;
            var refreshTicket = refreshTokenProtector.Unprotect(refreshDTO.RefreshToken);

            // Reject the /refresh attempt with a 401 if the token expired or the security stamp validation fails
            if (refreshTicket?.Properties?.ExpiresUtc is not { } expiresUtc ||
                _timeProvider.GetUtcNow() >= expiresUtc ||
                await _signInManager.ValidateSecurityStampAsync(refreshTicket.Principal) is not ApplicationUser user)
            {
                return new RefreshResultDTO
                {
                    IsTokenRefreshed = false,
                };
            }

            var newPrincipal = await _signInManager.CreateUserPrincipalAsync(user);

            return new RefreshResultDTO
            {
                IsTokenRefreshed = true,
                ClaimsPrincipal = newPrincipal,
                AuthenticationScheme = IdentityConstants.BearerScheme,
            };
        }

        public async Task<bool> RememberMachine(TwoFactorDTO twoFactorDTO, ClaimsPrincipal claimsPrincipal)
        {
            if (twoFactorDTO.ForgetMachine)
            {
                await _signInManager.ForgetTwoFactorClientAsync();
            }

            var user = await _userManager.GetUserAsync(claimsPrincipal);
            return await _signInManager.IsTwoFactorClientRememberedAsync(user!);
        }

        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}
