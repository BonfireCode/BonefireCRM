namespace BonefireCRM.Domain.DTOs.Contact
{
    public class CreateContactDTO
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string JobRole { get; set; } = string.Empty;
    }
}
