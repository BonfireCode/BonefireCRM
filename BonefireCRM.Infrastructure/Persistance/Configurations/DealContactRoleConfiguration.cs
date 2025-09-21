using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class DealContactRoleConfiguration : IEntityTypeConfiguration<DealContactRole>
    {
        public void Configure(EntityTypeBuilder<DealContactRole> entity)
        {
            entity.Property(r => r.Name).IsRequired().HasMaxLength(100);
            entity.Property(r => r.Description).HasMaxLength(500);

            entity.HasOne(r => r.RegisteredByUser)
                .WithMany()
                .HasForeignKey(r => r.RegisteredByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
