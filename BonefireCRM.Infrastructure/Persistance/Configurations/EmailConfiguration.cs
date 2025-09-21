using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class EmailConfiguration : IEntityTypeConfiguration<Domain.Entities.Email>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Email> entity)
        {
            entity.Property(e => e.Subject).IsRequired().HasMaxLength(500);
            entity.Property(e => e.Body).IsRequired();

            entity.HasIndex(e => e.SentAt);
        }
    }
}
