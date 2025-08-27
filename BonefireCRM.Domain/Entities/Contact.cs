using BonefireCRM.Domain.DTOs;

namespace BonefireCRM.Domain.Entities
{

    /// <summary>
    /// Represents a contact (an individual).
    /// </summary>
    public class Contact : BaseEntity
    {
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? JobTitle { get; set; }

        public Guid? CreatedByUserId { get; set; }
        public virtual User? CreatedByUser { get; set; }

        public Guid? CompanyId { get; set; }
        public virtual Company? Company { get; set; }

        // Navigation properties
        public virtual ICollection<Deal> Deals { get; set; } = [];
        public virtual ICollection<Interaction> Interactions { get; set; } = [];
        public virtual ICollection<FollowUpReminder> FollowUpReminders { get; set; } = [];
        public virtual ICollection<Tag> Tags { get; set; } = [];
    }
}