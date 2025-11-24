using BonefireCRM.Domain.Enums;

namespace BonefireCRM.Domain.DTOs.PipelineStage
{
    public class GetPipelineStageDTO
    {
        public Guid Id { get; set; }

        public Guid PipelineId { get; set; }

        public string Name { get; set; } = string.Empty;

        public int OrderIndex { get; set; }

        public DealClosureStatus? Status { get; set; }
    }
}
