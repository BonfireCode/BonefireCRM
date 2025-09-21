namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Represents a pipeline that organizes deals into stages.
    /// </summary>
    public class Pipeline : BaseEntity
    {
        public string Name { get; set; } = null!;
        public bool IsDefault { get; set; }

        // Navigation
        public ICollection<PipelineStage> Stages { get; set; } = new List<PipelineStage>();
    }
}