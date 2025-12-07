namespace BonefireCRM.Domain.DTOs.Pipeline
{
    public class GetPipelineListItemDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsDefault { get; set; }
    }
}
