using BonefireCRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace BonefireCRM.Infrastructure.Persistance
{
    public class CRMContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Interaction> Interactions { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<FollowUpReminder> FollowUpReminders { get; set; }
        public DbSet<Tag> Tags { get; set; }

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
