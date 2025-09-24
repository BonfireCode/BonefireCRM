namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Represents a scheduled or completed meeting.
    /// </summary>
    public class Meeting : Activity
    {
        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public string Subject { get; set; } = string.Empty;

        public string Notes { get; set; } = string.Empty;
    }
}
