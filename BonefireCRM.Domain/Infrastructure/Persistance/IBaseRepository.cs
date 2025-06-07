namespace BonefireCRM.Domain.Infrastructure.Persistance
{
    public interface IBaseRepository<T>
    {
        IEnumerable<T> GetAllAsync();
        Task<T?> GetAsync(Guid id, CancellationToken ct);
        Task<int> CreateAsync(T entity, CancellationToken ct);
        Task<int> UpdateAsync(Guid id, T entity, CancellationToken ct);
        Task<int> DeleteAsync(T entity, CancellationToken ct);
    }
}
