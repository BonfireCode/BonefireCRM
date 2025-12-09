using System.ComponentModel;

namespace BonefireCRM.API.Contrat.PipelineStage
{
    public sealed class GetPipelineStagesRequest
    {
        [Description("The unique identifier of the pipeline stage.")]
        public Guid? PipelineStageId { get; set; }

        [Description("The name of the pipeline stage.")]
        public string? Name { get; set; } = string.Empty;

        [Description("The order index that determines the position of this stage within the pipeline.")]
        public int? OrderIndex { get; set; }

        [Description("The filter status of the stage. Accepts the values 'Won', 'Lost', or empty.")]
        public string? Status { get; set; } = string.Empty;

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
