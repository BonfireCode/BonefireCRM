namespace BonefireCRM.Domain.DTOs.Activity.Task
{
    public class UpdateTaskDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? DealId { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public DateTime DueDate { get; set; }
        public bool IsCompleted { get; set; }
    }
}
