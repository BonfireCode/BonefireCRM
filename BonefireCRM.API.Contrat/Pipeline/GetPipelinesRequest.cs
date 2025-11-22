using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Pipeline
{
    public sealed class GetPipelinesRequest
    {
        [Description("The unique identifier of the pipeline.")]
        public Guid? Id { get; set; }

        [Description("The name of the pipeline.")]
        public string? Name { get; set; } = string.Empty;

        [Description("Indicates whether the pipeline is the default one.")]
        public bool? IsDefault { get; set; }

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
