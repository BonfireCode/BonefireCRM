using BonefireCRM.SourceGenerator;

namespace BonefireCRM.Domain.DTOs.Activity.Assignment
{
    [QueryExpressionsFor(typeof(Entities.Assignment))]
    public class GetAllAssignmentsDTO
    {
        public Guid? Id { get; set; }
        public string? Subject { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public DateTime? DueDate { get; set; }
        public bool? IsCompleted { get; set; }
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