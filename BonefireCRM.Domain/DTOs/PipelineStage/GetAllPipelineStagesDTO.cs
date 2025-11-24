using BonefireCRM.Domain.Enums;
using BonefireCRM.SourceGenerator;

namespace BonefireCRM.Domain.DTOs.PipelineStage
{
    [QueryExpressionsFor(typeof(Entities.PipelineStage))]
    public class GetAllPipelineStagesDTO
    {
        public Guid? Id { get; set; }
        public Guid? PipelineId { get; set; }
        public string? Name { get; set; } = string.Empty;
        public int? OrderIndex { get; set; }
        public DealClosureStatus? Status { get; set; }
        public string SortBy { get; set; } = string.Empty;
        public string SortDirection { get; set; } = string.Empty;
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }
}
