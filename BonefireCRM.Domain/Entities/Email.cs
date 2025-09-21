namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Represents an email interaction (incoming or outgoing).
    /// </summary>
    public class Email : Activity
    {
        public string Subject { get; set; } = null!;
        public string Body { get; set; } = null!;
        public DateTime SentAt { get; set; }
        public bool IsIncoming { get; set; }
    }
}
