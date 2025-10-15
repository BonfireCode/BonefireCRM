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

            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<Contact>()
                .WithMany()
                .HasForeignKey(d => d.PrimaryContactId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<Company>()
                .WithMany(c => c.Deals)
                .HasForeignKey(d => d.CompanyId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne<PipelineStage>()
                .WithMany(ps => ps.Deals)
                .HasForeignKey(d => d.PipelineStageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
