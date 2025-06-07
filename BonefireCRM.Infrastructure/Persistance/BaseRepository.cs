using BonefireCRM.Domain.Infrastructure.Persistance;

namespace BonefireCRM.Infrastructure.Persistance
{
    internal class BaseRepository<T> : IBaseRepository<T> where T : class
    {
        private readonly CRMContext _context;

        public BaseRepository(CRMContext context)
        {
            _context = context;
        }

        public IEnumerable<T> GetAllAsync()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public async Task<T?> GetAsync(Guid id, CancellationToken ct)
        {
            return await _context.FindAsync<T>(id, ct);
        }

        public async Task<int> CreateAsync(T entity, CancellationToken ct)
        {
            await _context.AddAsync(entity, ct);
            return await _context.SaveChangesAsync(ct);
        }

        public async Task<int> DeleteAsync(T entity, CancellationToken ct)
        {
            _context.Remove(entity);
            return await _context.SaveChangesAsync(ct);
        }

        public async Task<int> UpdateAsync(Guid id, T entity, CancellationToken ct)
        {
            _context.Update(entity);
            return await _context.SaveChangesAsync(ct);
        }
    }
}
