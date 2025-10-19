using System.Linq.Expressions;

namespace BonefireCRM.Domain.Infrastructure.Persistance
{
    public interface IBaseRepository<T>
    {
        Task<T?> GetAsync(Guid id, CancellationToken ct);
        Task<T> AddAsync(T entity, CancellationToken ct);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken ct);
        Task<T> UpdateAsync(T entity, CancellationToken ct);
        Task<bool> DeleteAsync(T entity, CancellationToken ct);
        Task<IEnumerable<T>> GetAllAsync(
            Expression<Func<T, bool>>? predicate = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int? skip = null,
            int? take = null,
            CancellationToken ct = default);
    }
}
