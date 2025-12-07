using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Pipeline
{
    public sealed class GetPipelineListItemResponse
    {
        [Description("The unique identifier of the pipeline.")]
        public Guid Id { get; set; }

        [Description("The name of the pipeline.")]
        public string Name { get; set; } = string.Empty;

        [Description("Indicates whether this pipeline is the default pipeline.")]
        public bool IsDefault { get; set; }
    }
}
