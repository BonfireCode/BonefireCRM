using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class DealParticipantRoleConfiguration : IEntityTypeConfiguration<DealParticipantRole>
    {
        public void Configure(EntityTypeBuilder<DealParticipantRole> entity)
        {
            entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
            entity.Property(r => r.Description).HasMaxLength(500);

            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(r => r.RegisteredByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
