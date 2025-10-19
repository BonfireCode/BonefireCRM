using BonefireCRM.Domain.DTOs.Shared;

namespace BonefireCRM.Domain.DTOs.Contact
{
    public class GetContactsDTO : PaginationFilterDTO
    {
        public Guid? Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
    }
}
