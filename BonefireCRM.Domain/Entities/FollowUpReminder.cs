namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Represents a reminder to follow up on an activity (task, call, meeting, or email).
    /// </summary>
    public class FollowUpReminder : BaseEntity
    {
        public string Note { get; set; } = string.Empty;

        public DateTime DueDate { get; set; }

        public bool IsCompleted { get; set; }

        public Guid ActivityId { get; set; }
    }
}