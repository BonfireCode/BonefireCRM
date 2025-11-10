namespace BonefireCRM.Domain.DTOs.Shared
{
    public class FilterGroup
    {
        public string Logic { get; set; } = "and";
        public List<Filter>? Filters { get; set; }
        public List<FilterGroup>? Groups { get; set; }
    }
}
