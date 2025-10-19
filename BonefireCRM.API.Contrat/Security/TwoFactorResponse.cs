using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Security
{
    public class TwoFactorResponse
    {
        [Description("The shared key used for TOTP authenticator apps, typically presented to the user as a QR code.")]
        public required string SharedKey { get; init; }

        [Description("The number of unused recovery codes remaining.")]
        public required int RecoveryCodesLeft { get; init; }

        [Description("The recovery codes to use if the shared key is lost. This property is only included if ResetRecoveryCodes was set or two-factor authentication was enabled for the first time.")]
        public string[]? RecoveryCodes { get; init; }

        [Description("Indicates whether two-factor login is required for the current authenticated user.")]
        public required bool IsTwoFactorEnabled { get; init; }

        [Description("Indicates whether the current client is remembered by two-factor authentication cookies. Always false for non-cookie authentication schemes.")]
        public required bool IsMachineRemembered { get; init; }
    }
}
