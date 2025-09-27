namespace BonefireCRM.Domain.Infrastructure.Persistance
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAllAsync();
        Task<T?> GetAsync(Guid id, CancellationToken ct);
        Task<T> AddAsync(T entity, CancellationToken ct);
        Task<ICollection<T>> AddRangeAsync(ICollection<T> entities, CancellationToken ct);
        Task<T> UpdateAsync(T entity, CancellationToken ct);
        Task<bool> DeleteAsync(T entity, CancellationToken ct);
    }
}
