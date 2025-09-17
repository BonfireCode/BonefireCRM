using BonefireCRM.Domain.Enums;
using System.Diagnostics;

namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Represents a follow-up reminder.
    /// </summary>
    public class FollowUpReminder : BaseEntity
    {
        public int ReminderId { get; set; }
        public string Note { get; set; } = null!;
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime CreatedAt { get; set; }

        public Guid ActivityId { get; set; }
        public Activity Activity { get; set; } = null!;
    }
}