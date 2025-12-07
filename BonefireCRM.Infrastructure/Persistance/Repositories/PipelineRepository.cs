using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace BonefireCRM.Infrastructure.Persistance.Repositories
{
    internal class PipelineRepository : BaseRepository<Pipeline>, IPipelineRepository
    {
        private readonly CRMContext _context;
        public PipelineRepository(CRMContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Pipeline?> GetPipelineIncludeStagesAsync(Guid id, CancellationToken ct)
        {
            return await _context.Pipelines
                .Include(d => d.Stages)
                .AsNoTracking()
                .SingleOrDefaultAsync(d => d.Id == id, ct);
        }
    }
}
