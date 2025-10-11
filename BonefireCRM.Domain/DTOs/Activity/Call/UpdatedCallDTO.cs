namespace BonefireCRM.Domain.DTOs.Activity.Call
{
    public class UpdatedCallDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ContactId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? DealId { get; set; }
        public DateTime CallTime { get; set; }
        public TimeSpan Duration { get; set; }
        public string Notes { get; set; } = string.Empty;
    }
}
