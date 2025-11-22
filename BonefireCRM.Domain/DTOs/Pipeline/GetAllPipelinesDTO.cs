using BonefireCRM.SourceGenerator;

namespace BonefireCRM.Domain.DTOs.Pipeline
{
    [QueryExpressionsFor(typeof(Entities.Pipeline))]
    public class GetAllPipelinesDTO
    {
        public Guid? Id { get; set; }
        public string? Name { get; set; } = string.Empty;
        public bool? IsDefault { get; set; }
        public string SortBy { get; set; } = string.Empty;
        public string SortDirection { get; set; } = string.Empty;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
