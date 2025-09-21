namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Represents a reminder to follow up on an activity (task, call, meeting, or email).
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