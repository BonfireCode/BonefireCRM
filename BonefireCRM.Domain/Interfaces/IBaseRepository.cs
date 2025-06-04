namespace BonefireCRM.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<int> CreateAsync(T entity);
        Task<int> UpdateAsync(Guid id, T entity);
        Task<int> DeleteAsync(Guid id);
    }
}
