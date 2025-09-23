using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class DealParticipantConfiguration : IEntityTypeConfiguration<DealParticipant>
    {
        public void Configure(EntityTypeBuilder<DealParticipant> entity)
        {
            entity.HasIndex(dc => new { dc.DealId, dc.ContactId }).IsUnique();
        }
    }
}
