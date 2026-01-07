using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class DealParticipantConfiguration : IEntityTypeConfiguration<DealParticipant>
    {
        public void Configure(EntityTypeBuilder<DealParticipant> entity)
        {
            entity.HasIndex(dp => new { dp.DealId, dp.ContactId, dp.DealParticipantRoleId }).IsUnique();

            entity.HasOne<Deal>()
                .WithMany(d => d.DealParticipants)
                .HasForeignKey(dp => dp.DealId)
                .OnDelete(DeleteBehavior.ClientCascade);

            entity.HasOne<Contact>()
                .WithMany(d => d.DealParticipants)
                .HasForeignKey(c => c.ContactId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<DealParticipantRole>()
                .WithMany(d => d.DealParticipants)
                .HasForeignKey(dp => dp.DealParticipantRoleId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
