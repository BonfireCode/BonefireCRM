namespace BonefireCRM.Domain.Entities
{
    public class PipelineStage : BaseEntity
    {
        public string Name { get; set; } = null!;
        public int OrderIndex { get; set; }
        public bool IsClosedWon { get; set; }
        public bool IsClosedLost { get; set; }

        public Guid PipelineId { get; set; }

        // Navigation
        public Pipeline Pipeline { get; set; } = null!;
        public ICollection<Deal> Deals { get; set; } = new List<Deal>();
    }
}