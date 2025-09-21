namespace BonefireCRM.Domain.DTOs.Security
{
    public class LoginResultDTO
    {
        public bool Succeeded { get; set; }

        public string State { get; set; } = string.Empty;
    }
}
