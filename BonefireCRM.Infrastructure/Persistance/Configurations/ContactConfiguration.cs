using BonefireCRM.Domain.Entities;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class ContactConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> entity)
        {
            entity.Property(c => c.FirstName)
            .IsRequired()
            .HasMaxLength(100);

            entity.Property(c => c.LastName)
            .IsRequired()
            .HasMaxLength(100);

            entity.Property(c => c.Email).HasMaxLength(200);
            entity.HasIndex(c => c.Email);

            entity.Property(c => c.PhoneNumber).HasMaxLength(50);
            entity.Property(c => c.JobRole).HasMaxLength(100);

            entity.HasOne(c => c.Company)
                .WithMany(co => co.Contacts)
                .HasForeignKey(c => c.CompanyId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne(c => c.LifecycleStage)
                .WithMany(ls => ls.Contacts)
                .HasForeignKey(c => c.LifecycleStageId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
