using BonefireCRM.Domain.Enums;

namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Represents a sales deal or opportunity.
    /// </summary>
    public class Deal : BaseEntity
    {
        public string Name { get; set; } = string.Empty;
        public decimal EstimatedValue { get; set; }
        public DateTime? ExpectedCloseDate { get; set; }
        public SalesStage Stage { get; set; } = SalesStage.Lead;

        public Guid PrimaryContactId { get; set; }
        public virtual Contact PrimaryContact { get; set; } = null!;

        public Guid? AssignedToUserId { get; set; }
        public virtual User? AssignedToUser { get; set; }

        public virtual ICollection<Interaction> Interactions { get; set; } = [];
        public virtual ICollection<Contact> AssociatedContacts { get; set; } = [];
        public virtual ICollection<FollowUpReminder> FollowUpReminders { get; set; } = [];
    }
}