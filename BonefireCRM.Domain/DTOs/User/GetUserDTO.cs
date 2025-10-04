using BonefireCRM.Domain.DTOs.Contact;

namespace BonefireCRM.Domain.DTOs.User
{
    public class GetUserDTO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public IEnumerable<GetContactDTO> Contacts { get; set; } = [];

        //public IEnumerable<GetActivityDTO> Activities { get; set; } = [];
    }
}
