using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Infrastructure.Persistance;
using BonefireCRM.Infrastructure.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static TBuilder AddInfrastructureDependencies<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            var connectionString = builder.Configuration.GetConnectionString("BonefireCRM_Db");

            builder.Services.AddDbContext<CRMContext>(options =>
            {
                options.UseSqlite(connectionString);
            });
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            
            builder.Services.AddAuthorizationBuilder()
                .SetDefaultPolicy(new AuthorizationPolicyBuilder().RequireAuthenticatedUser().Build());

            return builder;
        }

        public static IEndpointConventionBuilder MapIdentityApi(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapIdentityApi<ApplicationUser>().ExcludeRegister(); ;
        }
    }
}
