namespace BonefireCRM.Domain.DTOs.Security
{
    public class ConfirmEmailDTO
    {
        public string UserId { get; set; } = string.Empty;

        public string Token { get; set; } = string.Empty;

        public string ChangedEmail { get; set; } = string.Empty;
    }
}
