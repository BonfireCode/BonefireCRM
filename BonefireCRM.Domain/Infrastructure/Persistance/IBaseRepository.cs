using System.Linq.Expressions;

namespace BonefireCRM.Domain.Infrastructure.Persistance
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAll(Expression<Func<T, bool>> filterExpression, Expression<Func<T, object>> sortExpression, string sortDirection, int skip, int take, CancellationToken ct);
        Task<T?> GetByIdAsync(Guid id, CancellationToken ct);
        Task<T?> GetAsync(Expression<Func<T, bool>> predicate, CancellationToken ct);
        Task<T> AddAsync(T entity, CancellationToken ct);
        Task<IEnumerable<T>> AddRangeAsync(IEnumerable<T> entities, CancellationToken ct);
        Task<T> UpdateAsync(T entity, CancellationToken ct);
        Task<bool> DeleteAsync(T entity, CancellationToken ct);
    }
}
