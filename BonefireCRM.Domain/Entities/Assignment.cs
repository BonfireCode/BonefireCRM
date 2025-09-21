namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Represents a to-do or task assigned to a user.
    /// </summary>
    public class Assignment : Activity
    {
        public string Subject { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
