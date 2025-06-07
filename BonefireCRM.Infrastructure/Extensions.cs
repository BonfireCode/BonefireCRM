using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BonefireCRM.Infrastructure
{
    public static class Extensions
    {
        public static TBuilder AddInfrastructure<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            var connectionString = builder.Configuration.GetConnectionString("BonefireCRM_Db");
            builder.Services.AddDbContext<CRMContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            return builder;
        }
    }
}
