using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> entity)
        {
            entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
            entity.Property(c => c.Industry).HasMaxLength(100);
            entity.Property(c => c.Address).HasMaxLength(300);
            entity.Property(c => c.Phone).HasMaxLength(50);

            entity.HasMany(c => c.Contacts)
                .WithOne(cn => cn.Company)
                .HasForeignKey(cn => cn.CompanyId);

            entity.HasMany(c => c.Deals)
                .WithOne(d => d.Company)
                .HasForeignKey(d => d.CompanyId);
        }
    }
}
