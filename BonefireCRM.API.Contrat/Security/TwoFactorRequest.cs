using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Security
{
    public class TwoFactorRequest
    {
        [Description("Optional. Indicates whether to enable or disable the two-factor login requirement for the authenticated user. If null or not set, the current setting remains unchanged.")]
        public bool? Enable { get; init; }

        [Description("The two-factor code derived from the TwoFactorResponse.SharedKey. Required only if Enable is set to true.")]
        public string? TwoFactorCode { get; init; }

        [Description("Optional. Indicates whether to reset the TwoFactorResponse.SharedKey to a new random value. This action automatically disables two-factor login until it is re-enabled later.")]
        public bool ResetSharedKey { get; init; }

        [Description("Optional. Indicates whether to reset the TwoFactorResponse.RecoveryCodes to new random values. RecoveryCodes will be empty unless they are reset or two-factor is enabled for the first time.")]
        public bool ResetRecoveryCodes { get; init; }

        [Description("Optional. Indicates whether to clear the cookie 'remember me' flag if present. This has no effect on non-cookie authentication schemes.")]
        public bool ForgetMachine { get; init; }
    }
}
