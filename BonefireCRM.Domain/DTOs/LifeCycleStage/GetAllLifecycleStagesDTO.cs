using BonefireCRM.SourceGenerator;

namespace BonefireCRM.Domain.DTOs.LifeCycleStage
{
    [QueryExpressionsFor(typeof(Entities.LifecycleStage))]
    public class GetAllLifecycleStagesDTO
    {
        public Guid? Id { get; set; }

        public string? Name { get; set; } = string.Empty;

        public string SortBy { get; set; } = string.Empty;

        public string SortDirection { get; set; } = string.Empty;

        public int PageNumber { get; set; }

        public int PageSize { get; set; }
    }
}
