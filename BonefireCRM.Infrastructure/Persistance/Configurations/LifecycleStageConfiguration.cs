using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class LifecycleStageConfiguration : IEntityTypeConfiguration<LifecycleStage>
    {
        public void Configure(EntityTypeBuilder<LifecycleStage> entity)
        {
            entity.Property(ls => ls.Name)
                .IsRequired()
                .HasMaxLength(100);
        }
    }
}
