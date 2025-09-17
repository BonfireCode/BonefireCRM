using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class DealConfiguration : IEntityTypeConfiguration<Deal>
    {
        public void Configure(EntityTypeBuilder<Deal> entity)
        {
            entity.Property(d => d.Title).IsRequired().HasMaxLength(200);
            entity.Property(d => d.Amount).HasColumnType("decimal(18,2)");

            entity.HasOne(d => d.Stage)
                .WithMany(ps => ps.Deals)
                .HasForeignKey(d => d.StageId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne(d => d.Company)
                .WithMany(c => c.Deals)
                .HasForeignKey(d => d.CompanyId);

            entity.HasOne(d => d.PrimaryContact)
                .WithMany()
                .HasForeignKey(d => d.PrimaryContactId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(d => d.User)
                .WithMany()
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
