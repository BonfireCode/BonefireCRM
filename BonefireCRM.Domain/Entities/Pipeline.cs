namespace BonefireCRM.Domain.Entities
{
    /// <summary>
    /// Represents a pipeline that organizes deals into stages.
    /// </summary>
    public class Pipeline : BaseEntity
    {
        public string Name { get; set; } = string.Empty;

        public bool IsDefault { get; set; }

        public List<PipelineStage> Stages { get; set; } = [];
    }
}