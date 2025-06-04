namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Represents a tag that can be applied to entities like Contacts.
    /// </summary>
    public class Tag
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty; // Tag name, e.g., "VIP", "Lead", "FollowUpRequired"
        public string? ColorHex { get; set; } // Optional color for the tag in UI
        public DateTime DateCreated { get; set; } = DateTime.UtcNow;

        // Navigation property for many-to-many with Contact
        // EF Core can create a join table implicitly for this.
        public virtual ICollection<Contact> Contacts { get; set; } = [];
    }

    // --- DTOs for Dashboard/Reporting (as before) ---
    // ...
}