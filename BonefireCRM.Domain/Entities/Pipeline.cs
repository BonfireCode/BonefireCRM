namespace BonefireCRM.Domain.Entities
{
    public class Pipeline : BaseEntity
    {
        public string Name { get; set; } = null!;
        public bool IsDefault { get; set; }

        // Navigation
        public ICollection<PipelineStage> Stages { get; set; } = new List<PipelineStage>();
    }
}