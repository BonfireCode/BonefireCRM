namespace BonefireCRM.Domain.DTOs.Security
{
    public class GetInfoResultDTO
    {
        public string Email { get; set; } = string.Empty;

        public bool IsEmailConfirmed { get; set; }

        public KeyValuePair<string, string> ValidationError { get; set; }
    }
}
