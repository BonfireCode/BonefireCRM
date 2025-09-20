using System.Security.Claims;

namespace BonefireCRM.Domain.DTOs.Security
{
    public class RefreshResultDTO
    {
        public bool IsTokenRefreshed { get; set; }

        public ClaimsPrincipal ClaimsPrincipal { get; set; }

        public string AuthenticationScheme { get; set; } = string.Empty;
    }
}
