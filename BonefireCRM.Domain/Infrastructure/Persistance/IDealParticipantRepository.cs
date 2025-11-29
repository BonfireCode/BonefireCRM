using BonefireCRM.Domain.Entities;

namespace BonefireCRM.Domain.Infrastructure.Persistance
{
    public interface IDealParticipantRepository : IBaseRepository<DealParticipant>
    {
        Task<DealParticipant> GetDealParticipantAsync(Guid dealId, Guid participantId, CancellationToken ct);
    }
}
