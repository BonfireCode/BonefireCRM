using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class DealContactConfiguration : IEntityTypeConfiguration<DealContact>
    {
        public void Configure(EntityTypeBuilder<DealContact> entity)
        {
            entity.HasOne(dc => dc.Deal)
            .WithMany(d => d.DealContacts)
            .HasForeignKey(dc => dc.DealId);

            entity.HasOne(dc => dc.Contact)
                .WithMany(c => c.DealContacts)
                .HasForeignKey(dc => dc.ContactId);

            entity.HasOne(dc => dc.Role)
                .WithMany(r => r.DealContacts)
                .HasForeignKey(dc => dc.DealContactRoleId);

            entity.HasIndex(dc => new { dc.DealId, dc.ContactId }).IsUnique();
        }
    }
}
