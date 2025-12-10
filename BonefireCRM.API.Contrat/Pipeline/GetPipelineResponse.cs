using BonefireCRM.API.Contrat.Pipeline.Stage;
using System.ComponentModel;

namespace BonefireCRM.API.Contrat.Pipeline
{
    public sealed class GetPipelineResponse : PipelineSummary
    {
        [Description("A list of stages assigned to the pipeline.")]
        public IEnumerable<GetPipelineStageResponse> PipelineStages { get; set; } = [];
    }
}
