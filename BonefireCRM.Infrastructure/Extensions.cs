using BonefireCRM.Domain.Infrastructure.Email;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Domain.Infrastructure.Security;
using BonefireCRM.Infrastructure.Emailing;
using BonefireCRM.Infrastructure.Persistance;
using BonefireCRM.Infrastructure.Security;
using EntityFramework.Exceptions.SqlServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Routing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static TBuilder AddInfrastructureDependencies<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .WriteTo.Debug()
                .CreateLogger();
            builder.Services.AddSerilog();

            var connectionString = builder.Configuration.GetConnectionString("BonefireCRM_Db");

            builder.Services.AddDbContext<CRMContext>(options =>
            {
                options.UseSqlServer(connectionString).UseExceptionProcessor();
            });
            builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(connectionString).UseExceptionProcessor();
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

        public static IHost MigrateDatabases(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var crmContext = services.GetRequiredService<CRMContext>();
                    crmContext.Database.Migrate();
                    crmContext.Seed();

                    var appContext = services.GetRequiredService<AppDbContext>();
                    appContext.Database.Migrate();
                }
                catch (SqlException ex)
                {
                    throw new Exception($"SQLite migration failed: {ex.Message}", ex);
                }
                catch (InvalidOperationException ex)
                {
                    throw new Exception($"Invalid migration operation: {ex.Message}", ex);
                }
                catch (Exception ex)
                {
                    throw new Exception($"Unexpected migration error: {ex.Message}", ex);
                }
            }

            return host;
        }

        private static void AddAuth<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddSignInManager()
                .AddEntityFrameworkStores<AppDbContext>()
                .AddDefaultTokenProviders();

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultScheme = IdentityConstants.BearerScheme;
                options.DefaultAuthenticateScheme = "Identity.BearerAndApplication";
            })
                .AddScheme<AuthenticationSchemeOptions, CompositeAuthenticationHandler>("Identity.BearerAndApplication", null)
                .AddBearerToken(IdentityConstants.BearerScheme)
                .AddIdentityCookies();

            var authorizationPolicy = new AuthorizationPolicyBuilder()
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
