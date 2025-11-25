namespace BonefireCRM.Domain.Entities
{

    /// <summary>
    /// Represents a system user who can own contacts, create deals, and perform activities.
    /// </summary>
    public class User : BaseEntity
    {
        public required Guid RegisterId { get; set; }

        public string UserName { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;

        public string FirstName { get; set; } = string.Empty;

        public string LastName { get; set; } = string.Empty;

        public List<Contact> Contacts { get; set; } = [];

        public List<Activity> Activities { get; set; } = [];
    }
}