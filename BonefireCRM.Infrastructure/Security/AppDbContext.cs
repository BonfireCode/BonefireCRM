using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BonefireCRM.Infrastructure.Security
{
    internal class AppDbContext : IdentityUserContext<ApplicationUser, Guid>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) :
            base(options)
        {

        }
    }
}
