namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Represents an email interaction (incoming or outgoing).
    /// </summary>
    public class Email : Activity
    {
        public string Subject { get; set; } = string.Empty;

        public string Body { get; set; } = string.Empty;

        public DateTime SentAt { get; set; }

        public bool IsIncoming { get; set; }
    }
}
