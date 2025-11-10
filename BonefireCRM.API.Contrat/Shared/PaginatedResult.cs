namespace BonefireCRM.API.Contrat.Shared
{
    public class PaginatedResult<T>
    {
        public IEnumerable<T> Data { get; set; } = [];
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<string> FilterableFields { get; set; } = [];
    }
}
