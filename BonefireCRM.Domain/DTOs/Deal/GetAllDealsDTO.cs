using BonefireCRM.SourceGenerator;

namespace BonefireCRM.Domain.DTOs.Deal
{
    [QueryExpressionsFor(typeof(Entities.Deal))]
    public class GetAllDealsDTO
    {
        public Guid? Id { get; set; }
        public string? Title { get; set; } = string.Empty;
        public decimal? Amount { get; set; }
        public DateTime? ExpectedCloseDate { get; set; }
        public Guid? PipelineStageId { get; set; }
        public Guid? CompanyId { get; set; }
        public Guid? PrimaryContactId { get; set; }
        public Guid? UserId { get; set; }
        public string SortBy { get; set; } = string.Empty;
        public string SortDirection { get; set; } = string.Empty;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
