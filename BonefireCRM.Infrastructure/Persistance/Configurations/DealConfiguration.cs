using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class DealConfiguration : IEntityTypeConfiguration<Deal>
    {
        public void Configure(EntityTypeBuilder<Deal> builder)
        {
            builder
                .HasOne(d => d.PrimaryContact)
                .WithMany(c => c.Deals)
                .HasForeignKey(d => d.PrimaryContactId);

            builder
                .HasMany(d => d.AssociatedContacts)
                .WithMany();
        }
    }
}
