using BonefireCRM.Domain.DTOs.Security;
using System.Security.Claims;

namespace BonefireCRM.Domain.Infrastructure.Security
{
    public interface IAppSignInManager
    {
        Task<LoginResultDTO> PasswordSignInAsync(LoginDTO loginDTO);
        Task<RefreshResultDTO> RefreshUserToken(RefreshDTO refreshDTO);
        Task<bool> RememberMachine(TwoFactorDTO twoFactorDTO, ClaimsPrincipal claimsPrincipal);
        Task SignOutAsync();
    }
}
