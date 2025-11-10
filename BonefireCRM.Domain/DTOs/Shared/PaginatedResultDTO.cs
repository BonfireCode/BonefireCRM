namespace BonefireCRM.Domain.DTOs.Shared
{
    public class PaginatedResultDTO<T>
    {
        public IEnumerable<T> Data { get; set; } = [];
        public int TotalCount { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<string> FilterableFields { get; set; } = [];
    }
}
