using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Infrastructure.Persistance;

namespace BonefireCRM.Infrastructure.Persistance.Repositories
{
    internal class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly CRMContext _context;

        public UserRepository(CRMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Guid> GetUserIdAsync(Guid registerId, CancellationToken ct)
        {
            return _context.Users
                .Where(u => u.RegisterId == registerId)
                .Select(u => u.Id)
                .SingleOrDefault();
        }
    }
}
