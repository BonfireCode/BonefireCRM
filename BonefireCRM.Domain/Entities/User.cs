namespace BonefireCRM.Domain.Entities
{

    /// <summary>
    /// Represents a user of the CRM system.
    /// </summary>
    public class User : BaseEntity
    {
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string? LastName { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public bool IsActive { get; set; } = true;

        // Navigation properties
        public virtual ICollection<Contact> ContactsManaged { get; set; } = [];
        public virtual ICollection<Deal> DealsAssigned { get; set; } = [];
        public virtual ICollection<Interaction> InteractionsLogged { get; set; } = [];
        public virtual ICollection<FollowUpReminder> FollowUpRemindersAssigned { get; set; } = []; // Reminders assigned to this user
        public virtual ICollection<FollowUpReminder> FollowUpRemindersCreated { get; set; } = []; // Reminders created by this user
    }
}