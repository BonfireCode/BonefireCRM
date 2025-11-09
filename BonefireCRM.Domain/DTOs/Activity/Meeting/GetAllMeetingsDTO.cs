using BonefireCRM.SourceGenerator;

namespace BonefireCRM.Domain.DTOs.Activity.Meeting
{
    [QueryExpressionsFor(typeof(Entities.Meeting))]
    public class GetAllMeetingsDTO
    {
        public Guid? Id { get; set; }
        public string? Subject { get; set; } = string.Empty;
        public string? Notes { get; set; } = string.Empty;
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public Guid? UserId { get; set; }
        public Guid? ContactId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? DealId { get; set; }
        public string SortBy { get; set; } = string.Empty;
        public string SortDirection { get; set; } = string.Empty;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}