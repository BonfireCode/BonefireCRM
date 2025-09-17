using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class PipelineConfiguration : IEntityTypeConfiguration<Pipeline>
    {
        public void Configure(EntityTypeBuilder<Pipeline> entity)
        {
            entity.Property(p => p.Name).IsRequired().HasMaxLength(150);

            entity.HasMany(p => p.Stages)
                .WithOne(ps => ps.Pipeline)
                .HasForeignKey(ps => ps.PipelineId);
        }
    }
}
