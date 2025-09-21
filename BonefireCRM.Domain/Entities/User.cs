using System.Diagnostics;

namespace BonefireCRM.Domain.Entities
{

    /// <summary>
    /// Represents a system user who can own contacts, create deals, and perform activities.
    /// </summary>
    public class User : BaseEntity
    {
        public string RegisterId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}