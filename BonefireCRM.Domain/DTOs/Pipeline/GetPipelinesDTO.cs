namespace BonefireCRM.Domain.DTOs.Pipeline
{
    public class GetPipelinesDTO
    {
        public IEnumerable<GetPipelineSummaryDTO> Pipelines { get; set; } = [];
    }
}
