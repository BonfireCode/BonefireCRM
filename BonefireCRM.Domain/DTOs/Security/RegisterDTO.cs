using BonefireCRM.Domain.Entities;

namespace BonefireCRM.Domain.DTOs.Security
{
    public class RegisterDTO
    {
        public string UserName { get; init; } = string.Empty;

        public string Email { get; init; } = string.Empty;

        public string Password { get; init; } = string.Empty;
    }
}
