using BonefireCRM.Domain.Infrastructure.Email;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Infrastructure.Security;
using BonefireCRM.Infrastructure.Email;
using BonefireCRM.Infrastructure.Persistance;
using BonefireCRM.Infrastructure.Security;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
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

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlite(connectionString);
            });

            builder.AddAuth();

            builder.Services.AddScoped<IAppUserManager, AppUserManager>();
            builder.Services.AddScoped<IAppSignInManager, AppSignInManager>();

            builder.Services.AddScoped<IAppHttpContextAccessor, AppHttpContextAccessor>();
            builder.Services.AddScoped<IEmailSender, EmailSender>();

            return builder;
        }

        public static IEndpointConventionBuilder MapIdentityApi(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapIdentityApi<ApplicationUser>();
        }

        private static void AddAuth<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddSignInManager()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication()
                .AddBearerToken(IdentityConstants.BearerScheme)
                .AddIdentityCookies();

            var authorizationPolicy = new AuthorizationPolicyBuilder()
                .AddAuthenticationSchemes(
                    IdentityConstants.BearerScheme,
                    IdentityConstants.ApplicationScheme)
                .RequireAuthenticatedUser()
                .Build();
            builder.Services.AddAuthorizationBuilder()
                .SetDefaultPolicy(authorizationPolicy);

            builder.Services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 0;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            builder.Services.ConfigureApplicationCookie(options =>
            {
                // Cookie settings
                options.Cookie.HttpOnly = true;
                options.ExpireTimeSpan = TimeSpan.FromMinutes(5);

                options.LoginPath = "/login";
                //options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;
            });
        }
    }
}
