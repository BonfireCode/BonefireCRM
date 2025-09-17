namespace BonefireCRM.Domain.Entities
{

    /// <summary>
    /// Represents a company associated with contacts.
    /// </summary>
    public class Company : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public string? Industry { get; set; }

        public string? Address { get; set; }

        public string? Phone { get; set; }

        public DateTime CreatedAt { get; set; }

        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();

        public ICollection<Deal> Deals { get; set; } = new List<Deal>();
    }
}