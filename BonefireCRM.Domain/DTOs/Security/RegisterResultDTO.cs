namespace BonefireCRM.Domain.DTOs.Security
{
    public class RegisterResultDTO
    {
        public Guid UserId { get; set; }

        public KeyValuePair<string, string> ValidationError { get; set; }
    }
}
