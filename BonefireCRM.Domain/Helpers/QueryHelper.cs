namespace BonefireCRM.Domain.Helpers
{
    internal static class QueryHelper
    {
        public static IQueryable<T> ApplySorting<T>(
        this IQueryable<T> query,
        string? sortBy,
        string? sortDirection)
        {
            if (string.IsNullOrWhiteSpace(sortBy))
                return query;

            bool ascending = string.Equals(sortDirection, "asc", StringComparison.OrdinalIgnoreCase);
            return ascending
                ? query.OrderByDynamic(sortBy)
                : query.OrderByDescendingDynamic(sortBy);
        }

        public static IQueryable<T> ApplyPagination<T>(
            this IQueryable<T> query,
            int pageNumber,
            int pageSize)
        {
            int skip = (pageNumber - 1) * pageSize;
            return query.Skip(skip).Take(pageSize);
        }
    }
}
