using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class MeetingConfiguration : IEntityTypeConfiguration<Meeting>
    {
        public void Configure(EntityTypeBuilder<Meeting> entity)
        {
            entity.Property(m => m.Subject).IsRequired().HasMaxLength(200);
            entity.Property(m => m.Notes).HasMaxLength(2000);
        }
    }
}
