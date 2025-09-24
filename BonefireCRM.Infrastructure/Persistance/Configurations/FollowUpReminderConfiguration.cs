using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class FollowUpReminderConfiguration : IEntityTypeConfiguration<FollowUpReminder>
    {
        public void Configure(EntityTypeBuilder<FollowUpReminder> entity)
        {
            entity.Property(r => r.Note).IsRequired().HasMaxLength(500);

            entity.HasOne<Activity>()
                .WithMany()
                .HasForeignKey(r => r.ActivityId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(r => r.DueDate);
        }
    }
}
