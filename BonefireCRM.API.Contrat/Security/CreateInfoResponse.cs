using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Security
{
    public class CreateInfoResponse
    {
        [Description("The email address associated with the authenticated user.")]
        public required string Email { get; init; }

        [Description("Indicates whether the email address has been confirmed.")]
        public required bool IsEmailConfirmed { get; init; }
    }
}
