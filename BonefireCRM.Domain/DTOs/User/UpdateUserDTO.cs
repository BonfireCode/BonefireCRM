namespace BonefireCRM.Domain.DTOs.User
{
    public class UpdateUserDTO
    {
        public Guid Id { get; set; }
        public string RegisterId { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
    }
}
