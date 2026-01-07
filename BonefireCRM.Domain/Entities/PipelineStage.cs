using BonefireCRM.Domain.Enums;

namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Represents a single stage within a pipeline (e.g., Qualification, Proposal).
    /// </summary>
    /// 
    public class PipelineStage : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public int OrderIndex { get; set; }

        public DealClosureStatus? Status { get; set; }

        public string LossReason { get; set; } = string.Empty;

        public Guid PipelineId { get; set; }

        public List<Deal> Deals { get; set; } = [];
    }
}