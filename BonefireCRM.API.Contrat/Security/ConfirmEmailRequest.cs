namespace BonefireCRM.API.Contrat.Security
{
    public class ConfirmEmailRequest
    {
        public required string UserId { get; init; }

        public required string Token { get; init; }

        public required string ChangedEmail { get; init; }
    }
}
