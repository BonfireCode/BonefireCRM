using BonefireCRM.Domain.Entities;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BonefireCRM.Infrastructure.Persistance
{
    internal class DealRepository : BaseRepository<Deal>, IDealRepository
    {
        private readonly CRMContext _context;
        public DealRepository(CRMContext context) :
            base(context)
        {
            _context = context;
        }

        public async Task<Deal?> GetDealWithParticipantsAsync(Guid id, CancellationToken ct)
        {
            return await _context.Deals
                .Include(d => d.DealParticipants)
                .AsNoTracking()
                .SingleOrDefaultAsync(d => d.Id == id, ct);
        }

        public async Task<Deal?> UpdateDealAsync(Deal deal, CancellationToken ct)
        {
            var entityFound = _context.Set<Deal>()
                .Include(d => d.DealParticipants)
                .SingleOrDefault(x => x.Id == deal.Id);

            if (entityFound is null)
            {
                return null;
            }

            _context.Entry(entityFound).CurrentValues.SetValues(deal);
            UpdateDealParticipants(deal, entityFound);

            await _context.SaveChangesAsync(ct);

            return deal;
        }

        private void UpdateDealParticipants(Deal deal, Deal entityFound)
        {
            var incomingParticipantIds = deal.DealParticipants.Select(dp => dp.Id).ToHashSet();
            var existingParticipantIds = entityFound.DealParticipants.Select(dp => dp.Id);

            var removedParticipants = entityFound.DealParticipants
                .Where(dp => !incomingParticipantIds.Contains(dp.Id));

            if (removedParticipants.Any())
            {
                _context.Set<DealParticipant>().RemoveRange(removedParticipants);
            }

            var addedParticipants = deal.DealParticipants
                .Where(dp => !existingParticipantIds.Contains(dp.Id))
                .Select(dp => new DealParticipant
                {
                    DealId = entityFound.Id,
                    ContactId = dp.ContactId,
                    DealParticipantRoleId = dp.DealParticipantRoleId
                });

            if (addedParticipants.Any())
            {
                _context.Set<DealParticipant>().AddRange(addedParticipants);
            }
        }
    }
}