namespace BonefireCRM.Domain.DTOs.Shared
{
    public class SortDefinition
    {
        public string Field { get; set; } = string.Empty;   // e.g. "Name"
        public string Direction { get; set; } = "asc";
    }
}