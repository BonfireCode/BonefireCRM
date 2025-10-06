namespace BonefireCRM.API.Contrat.Meeting
{
    public sealed class GetMeetingResponse
    {
        public Guid Id { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? DealId { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
    }
}
