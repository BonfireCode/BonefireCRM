namespace BonefireCRM.Domain.DTOs.Activity.Meeting
{
    public class CreatedMeetingDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? DealId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
