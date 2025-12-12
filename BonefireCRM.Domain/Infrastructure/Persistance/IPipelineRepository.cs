using BonefireCRM.Domain.Entities;

namespace BonefireCRM.Domain.Infrastructure.Persistance
{
    public interface IPipelineRepository : IBaseRepository<Pipeline>
    {
        Task<Pipeline?> GetPipelineIncludeStagesAsync(Guid id, CancellationToken ct);
    }
}
