using BonefireCRM.Domain.DTOs.Pipeline.Stage;

namespace BonefireCRM.Domain.DTOs.Pipeline
{
    public class GetPipelineDTO : PipelineSummaryDTO
    {
        public IEnumerable<GetPipelineStageDTO> PipelineStages { get; set; } = [];
    }
}
