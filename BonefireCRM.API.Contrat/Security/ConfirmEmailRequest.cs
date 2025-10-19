using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Security
{
    public class ConfirmEmailRequest
    {
        [Description("The unique identifier of the user whose email is being confirmed.")]
        public required string UserId { get; init; }

        [Description("The confirmation token sent to the user's email address.")]
        public required string Token { get; init; }

        [Description("The new email address to confirm, if the user has changed their email.")]
        public string ChangedEmail { get; init; } = string.Empty;
    }
}
