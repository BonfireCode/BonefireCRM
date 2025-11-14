using System.ComponentModel;

namespace BonefireCRM.API.Contrat.LifeCycleStage
{
    public sealed class GetLifecycleStagesRequest
    {
        [Description("Optional. Filters the lifecycle stages by their unique identifier.")]
        public Guid? Id { get; set; }

        [Description("Optional. Filters the lifecycle stages by their name.")]
        public string? Name { get; set; } = string.Empty;

        [Description("The sort by property name.")]
        public string? SortBy { get; set; }

        [Description("The sort direction: asc or desc.")]
        public string? SortDirection { get; set; }

        [Description("The page number.")]
        public int? PageNumber { get; set; }

        [Description("The page size.")]
        public int? PageSize { get; set; }
    }
}
