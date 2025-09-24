namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Represents a phone call logged in the CRM.
    /// </summary>
    public class Call : Activity
    {
        public DateTime CallTime { get; set; }

        public TimeSpan Duration { get; set; }

        public string Notes { get; set; } = string.Empty;
    }
}
