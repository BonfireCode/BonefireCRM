using BonefireCRM.Domain.Entities;
using BonefireCRM.Domain.Infrastructure.Persistance;

namespace BonefireCRM.Domain.Services
{
    public class SeedUserDataService
    {
        private readonly IBaseRepository<DealParticipantRole> _dealParticipantRoleRepository;

        public SeedUserDataService(IBaseRepository<DealParticipantRole> dealParticipantRoleRepository)
        {
            _dealParticipantRoleRepository = dealParticipantRoleRepository;
        }

        public async Task DealParticipantRolesAsync(User user, CancellationToken ct)
        {
            var defaultRoles = new List<DealParticipantRole>
            {
                new() { Name = "Decision Maker", Description = "Responsible for final decision-making in the deal", RegisteredByUserId = user.Id  },
                new() { Name = "Influencer", Description = "Influences the decision but does not have final authority", RegisteredByUserId = user.Id },
                new() { Name = "Evaluator", Description = "Evaluates the proposal and provides feedback", RegisteredByUserId = user.Id },
                new() { Name = "End User", Description = "Will ultimately use the product or service", RegisteredByUserId = user.Id }
            };

            await _dealParticipantRoleRepository.AddRangeAsync(defaultRoles, ct);
        }
    }
}
