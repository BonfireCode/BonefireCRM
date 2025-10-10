using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class ActivityConfiguration : IEntityTypeConfiguration<Activity>
    {
        public void Configure(EntityTypeBuilder<Activity> entity)
        {
            // Activity inheritance (TPH)
            entity.HasDiscriminator<string>("ActivityType")
                .HasValue<Call>(nameof(Call))
                .HasValue<Email>(nameof(Email))
                .HasValue<Meeting>(nameof(Meeting))
                .HasValue<Assignment>(nameof(Assignment));

            entity.HasOne<User>()
                .WithMany(u => u.Activities)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<Contact>()
                .WithMany()
                .HasForeignKey(a => a.ContactId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne<Company>()
                .WithMany()
                .HasForeignKey(a => a.CompanyId)
                .OnDelete(DeleteBehavior.SetNull);

            entity.HasOne<Deal>()
                .WithMany()
                .HasForeignKey(a => a.DealId)
                .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
