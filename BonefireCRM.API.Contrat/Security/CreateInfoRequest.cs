using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Security
{
    public class CreateInfoRequest
    {
        [Description("Optional. The new email address for the authenticated user. This will replace the old email address if there was one. The email will not be updated until it is confirmed.")]
        public string NewEmail { get; init; } = string.Empty;

        [Description("Optional. The new password for the authenticated user. If a new password is provided, the OldPassword is required. If the user forgot the old password, use the '/forgotPassword' endpoint instead.")]
        public string NewPassword { get; init; } = string.Empty;

        [Description("The old password for the authenticated user. This is required only if a NewPassword is provided.")]
        public string OldPassword { get; init; } = string.Empty;
    }
}
