namespace BonefireCRM.Domain.DTOs.Security
{
    public class LoginDTO
    {
        public string UserName { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;

        public string TwoFactorCode { get; init; } = string.Empty;

        public string TwoFactorRecoveryCode { get;  init; } = string.Empty;

        public bool UseCookies { get;  init; }

        public bool UseSessionCookies { get; init; }
    }
}
