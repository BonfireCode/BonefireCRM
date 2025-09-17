namespace BonefireCRM.Domain.Entities
{
    public class Assignment : Activity
    {
        public string Subject { get; set; } = null!;
        public string? Description { get; set; }
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
