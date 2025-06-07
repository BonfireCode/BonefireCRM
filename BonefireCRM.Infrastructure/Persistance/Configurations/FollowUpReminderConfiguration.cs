using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class FollowUpReminderConfiguration : IEntityTypeConfiguration<FollowUpReminder>
    {
        public void Configure(EntityTypeBuilder<FollowUpReminder> builder)
        {
            builder
                .HasOne(f => f.CreatedByUser)
                .WithMany(u => u.FollowUpRemindersCreated)
                .HasForeignKey(d => d.CreatedByUserId);

            builder
                .HasOne(f => f.AssignedToUser)
                .WithMany(u => u.FollowUpRemindersAssigned)
                .HasForeignKey(d => d.AssignedToUserId);
        }
    }
}
