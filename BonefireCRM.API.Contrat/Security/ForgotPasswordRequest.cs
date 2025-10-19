using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Security
{
    public class ForgotPasswordRequest
    {
        [Description("The email address to send the reset password code to, if a user with that confirmed email address already exists.")]
        public required string Email { get; init; }
    }
}
