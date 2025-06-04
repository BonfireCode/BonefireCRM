using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class ContactTypeConfiguration : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder
                .Property(c => c.FirstName)
                .IsRequired();

            builder
                .Property(c => c.LastName)
                .IsRequired();
        }
    }
}
