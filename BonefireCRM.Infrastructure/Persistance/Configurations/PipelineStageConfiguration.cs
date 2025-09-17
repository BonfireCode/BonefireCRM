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
    internal class PipelineStageConfiguration : IEntityTypeConfiguration<PipelineStage>
    {
        public void Configure(EntityTypeBuilder<PipelineStage> entity)
        {
            entity.Property(ps => ps.Name).IsRequired().HasMaxLength(150);

            entity.HasIndex(ps => new { ps.PipelineId, ps.OrderIndex }).IsUnique();

            entity.HasOne(ps => ps.Pipeline)
                .WithMany(p => p.Stages)
                .HasForeignKey(ps => ps.PipelineId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
