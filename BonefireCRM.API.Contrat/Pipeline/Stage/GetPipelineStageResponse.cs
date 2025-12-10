using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Pipeline.Stage
{
    public sealed class GetPipelineStageResponse
    {
        [Description("The unique identifier of the pipeline stage.")]
        public Guid Id { get; set; }

        [Description("The name of the pipeline stage.")]
        public string Name { get; set; } = string.Empty;

        [Description("The order index that determines the position of this stage within the pipeline.")]
        public int OrderIndex { get; set; }

        [Description("The status of the stage. Contains either 'Won', 'Lost', or is empty.")]
        public string Status { get; set; } = string.Empty;
    }
}
