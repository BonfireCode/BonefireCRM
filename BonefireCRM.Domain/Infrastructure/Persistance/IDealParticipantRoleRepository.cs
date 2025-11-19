using BonefireCRM.Domain.Entities;

namespace BonefireCRM.Domain.Infrastructure.Persistance
{
    public interface IDealParticipantRoleRepository : IBaseRepository<DealParticipantRole>
    {
        Task<DealParticipantRole?> GetDealParticipantRoleAsync(Guid id, Guid registeredByUserId, CancellationToken ct);
    }
}
