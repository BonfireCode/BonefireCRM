using BonefireCRM.SourceGenerator;

namespace BonefireCRM.Domain.DTOs.Activity.Call
{
    [QueryExpressionsFor(typeof(Entities.Call))]
    public class GetAllCallsDTO
    {
        public Guid? Id { get; set; }
        public DateTime? CallTime { get; set; }
        public TimeSpan? Duration { get; set; }
        public string? Notes { get; set; } = string.Empty;
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