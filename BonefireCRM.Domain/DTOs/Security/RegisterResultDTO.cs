namespace BonefireCRM.Domain.DTOs.Security
{
    public class RegisterResultDTO
    {
        public string UserId { get; set; } = string.Empty;

        public KeyValuePair<string, string> ValidationError { get; set; }
    }
}
