using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Assignment = BonefireCRM.Domain.Entities.Assignment;

namespace BonefireCRM.Infrastructure.Persistance
{
    public class CRMContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<LifecycleStage> LifecycleStages { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<DealContactRole> DealContactRoles { get; set; }
        public DbSet<DealContact> DealContacts { get; set; }
        public DbSet<Pipeline> Pipelines { get; set; }
        public DbSet<PipelineStage> PipelineStages { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<Call> Calls { get; set; }
        public DbSet<Meeting> Meetings { get; set; }
        public DbSet<Assignment> Tasks { get; set; }
        public DbSet<Domain.Entities.Email> Emails { get; set; }
        public DbSet<FollowUpReminder> FollowUpReminders { get; set; }

        public CRMContext(DbContextOptions<CRMContext> options) : base(options)
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
