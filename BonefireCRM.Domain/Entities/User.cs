using System.Diagnostics;

namespace BonefireCRM.Domain.Entities
{

    /// <summary>
    /// Represents a user of the CRM system.
    /// </summary>
    public class User : BaseEntity
    {

        // Navigation
        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();
    }
}