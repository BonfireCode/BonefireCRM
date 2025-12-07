using BonefireCRM.Domain.DTOs.Pipeline.Stage;

namespace BonefireCRM.Domain.DTOs.Pipeline
{
    public class GetPipelineDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
        public IEnumerable<GetPipelineStageDTO> PipelineStages { get; set; } = [];
    }
}
