namespace BonefireCRM.API.Contrat.Security
{
    public class ResendConfirmationRequest
    {
        /// <summary>
        /// The email address to resend the confirmation email to if a user with that email exists.
        /// </summary>
        public string Email { get; init; } = string.Empty;
    }
}
