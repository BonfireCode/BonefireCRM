using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BonefireCRM.Infrastructure.Persistance.Configurations
{
    internal class DealParticipantRoleConfiguration : IEntityTypeConfiguration<DealParticipantRole>
    {
        public void Configure(EntityTypeBuilder<DealParticipantRole> entity)
        {
            entity.Property(c => c.Name).IsRequired().HasMaxLength(200);
            entity.Property(c => c.Description).IsRequired().HasMaxLength(200);

            entity.HasOne<User>()
                .WithMany()
                .HasForeignKey(dpr => dpr.RegisteredByUserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
