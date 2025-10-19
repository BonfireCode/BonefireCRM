using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Security
{
    public class ResendConfirmationRequest
    {
        [Description("The email address to resend the confirmation email to, if a user with that email exists.")]
        public string Email { get; init; } = string.Empty;
    }
}
