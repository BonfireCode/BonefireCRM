namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Represents a tag that can be applied to entities like Contacts.
    /// </summary>
    public class Tag : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public string? ColorHex { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; } = [];
    }
}