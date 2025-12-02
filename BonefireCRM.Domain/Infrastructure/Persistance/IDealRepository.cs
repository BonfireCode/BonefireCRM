using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Infrastructure.Persistance;

namespace Microsoft.Extensions.DependencyInjection
{
    public interface IDealRepository : IBaseRepository<Deal>
    {
        Task<Deal?> GetDealWithParticipantsAsync(Guid id, CancellationToken ct);

        Task<Deal?> UpdateDealAsync(Deal deal, CancellationToken ct);
    }
}