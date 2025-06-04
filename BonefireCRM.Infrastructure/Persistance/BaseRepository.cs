using BonefireCRM.Domain.Interfaces;

namespace BonefireCRM.Infrastructure.Persistance
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        public Task<int> CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<int> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> UpdateAsync(Guid id, T entity)
        {
            throw new NotImplementedException();
        }
    }
}
