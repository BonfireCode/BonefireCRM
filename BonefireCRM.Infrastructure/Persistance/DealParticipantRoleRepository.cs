using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace BonefireCRM.Infrastructure.Persistance
{
    internal class DealParticipantRoleRepository : BaseRepository<DealParticipantRole>, IDealParticipantRoleRepository
    {
        private readonly CRMContext _context;

        public DealParticipantRoleRepository(CRMContext context) : base(context)
        {
            _context = context;
        }

        public Task<DealParticipantRole?> GetDealParticipantRoleAsync(Guid id, Guid registeredByUserId, CancellationToken ct)
        {
            return _context.DealParticipantRoles
                .Where(dpr => dpr.Id == id && dpr.RegisteredByUserId == registeredByUserId)
                .FirstOrDefaultAsync(ct);
        }
    }
}
