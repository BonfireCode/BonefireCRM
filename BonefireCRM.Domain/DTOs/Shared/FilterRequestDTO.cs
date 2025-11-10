namespace BonefireCRM.Domain.DTOs.Shared
{
    public class FilterRequestDTO
    {
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public List<SortDefinition>? Sort { get; set; }
        public FilterGroup? Filter { get; set; }
    }
}
