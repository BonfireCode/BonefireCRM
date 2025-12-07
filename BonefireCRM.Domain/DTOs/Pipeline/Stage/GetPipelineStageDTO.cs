using BonefireCRM.Domain.Enums;

namespace BonefireCRM.Domain.DTOs.Pipeline.Stage
{
    public class GetPipelineStageDTO
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = string.Empty;

        public int OrderIndex { get; set; }

        public DealClosureStatus? Status { get; set; }
    }
}
