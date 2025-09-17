namespace BonefireCRM.Domain.Entities
{
    public class Meeting : Activity
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Subject { get; set; } = null!;
        public string? Notes { get; set; }
    }
}
