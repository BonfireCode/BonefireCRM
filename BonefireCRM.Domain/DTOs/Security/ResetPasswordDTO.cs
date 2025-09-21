namespace BonefireCRM.Domain.DTOs.Security
{
    public class ResetPasswordDTO
    {
        public string Email { get; set; } = string.Empty;

        public string ResetCode { get; set; } = string.Empty;

        public string NewPassword { get; set; } = string.Empty;
    }
}
