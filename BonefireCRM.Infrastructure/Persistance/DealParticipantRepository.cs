using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace BonefireCRM.Infrastructure.Persistance
{
    internal class DealParticipantRepository : BaseRepository<DealParticipant>, IDealParticipantRepository
    {
        private readonly CRMContext _context;

        public DealParticipantRepository(CRMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<DealParticipant?> GetDealParticipantAsync(Guid dealId, Guid participantId, CancellationToken ct)
        {
            return await _context.DealParticipants
                .Where(d => d.DealId == dealId && d.ContactId == participantId)
                .SingleOrDefaultAsync(ct);
        }
    }
}
