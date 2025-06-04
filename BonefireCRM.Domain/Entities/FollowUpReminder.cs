using BonefireCRM.Domain.Enums;

namespace BonefireCRM.Domain.Entities
{
    // --- New Entities for FollowUpReminder and Tags ---

    /// <summary>
    /// Represents a follow-up reminder.
    /// </summary>
    public class FollowUpReminder
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty; // A brief title for the reminder
        public string? Notes { get; set; }
        public DateTime ReminderDateTime { get; set; } // When the reminder is due
        public bool IsCompleted { get; set; } = false;
        public DateTime? CompletedDateTime { get; set; }
        public ReminderPriority Priority { get; set; } = ReminderPriority.Normal;

        public DateTime DateCreated { get; set; } = DateTime.UtcNow;
        public DateTime LastModifiedDate { get; set; } = DateTime.UtcNow;

        // Foreign Key for the User who created the reminder
        public Guid CreatedByUserId { get; set; }
        public virtual User CreatedByUser { get; set; } = null!;

        // Foreign Key for the User assigned to complete the reminder (can be same as creator)
        public Guid? AssignedToUserId { get; set; }
        public virtual User? AssignedToUser { get; set; }

        // Foreign Key to link to a Contact (a reminder is often about a contact)
        public Guid? ContactId { get; set; }
        public virtual Contact? Contact { get; set; }

        // Foreign Key to link to a Deal (a reminder can also be about a deal)
        public Guid? DealId { get; set; }
        public virtual Deal? Deal { get; set; }
    }

    // --- DTOs for Dashboard/Reporting (as before) ---
    // ...
}