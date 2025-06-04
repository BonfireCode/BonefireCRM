namespace BonefireCRM.Domain.Entities
{

    /// <summary>
    /// Represents a company associated with contacts.
    /// </summary>
    public class Company : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? Website { get; set; }
        public string? Industry { get; set; }
        public string? Address { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; } = [];
    }
}