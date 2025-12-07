using BonefireCRM.Domain.Entities;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace BonefireCRM.Infrastructure.Persistance.Repositories
{
    internal class DealRepository : BaseRepository<Deal>, IDealRepository
    {
        private readonly CRMContext _context;
        public DealRepository(CRMContext context) :
            base(context)
        {
            _context = context;
        }

        public async Task<Deal?> GetDealIncludeParticipantsAsync(Guid id, CancellationToken ct)
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
            ManageDealParticipants(deal, entityFound);

            await _context.SaveChangesAsync(ct);

            return deal;
        }

        private void ManageDealParticipants(Deal deal, Deal entityFound)
        {
            _context.DealParticipants.RemoveRange(entityFound.DealParticipants);
            entityFound.DealParticipants.Clear();

            foreach (var dealParticipant in deal.DealParticipants)
            {
                if (dealParticipant.Id != Guid.Empty)
                    _context.Entry(dealParticipant).State = EntityState.Modified;
                else
                    _context.Entry(dealParticipant).State = EntityState.Added;

                entityFound.DealParticipants.Add(dealParticipant);
            }
        }
    }
}