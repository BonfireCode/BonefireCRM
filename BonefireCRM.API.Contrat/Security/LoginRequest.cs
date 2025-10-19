using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Security
{
    public sealed class LoginRequest
    {
        [Description("The user's username.")]
        public required string UserName { get; init; }

        [Description("The user's password.")]
        public required string Password { get; init; }

        [Description("Optional. The two-factor authenticator code. This may be required for users who have enabled two-factor authentication. Not required if a TwoFactorRecoveryCode is provided.")]
        public string TwoFactorCode { get; init; } = string.Empty;

        [Description("Optional. A two-factor recovery code from TwoFactorResponse.RecoveryCodes. Required for users who have enabled two-factor authentication but lost access to their TwoFactorCode.")]
        public string TwoFactorRecoveryCode { get; init; } = string.Empty;
    }
}
