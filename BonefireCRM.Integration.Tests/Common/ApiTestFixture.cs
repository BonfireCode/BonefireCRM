using BonefireCRM.API;
using BonefireCRM.Domain.Infrastructure.Persistance;
using BonefireCRM.Infrastructure.Persistance;
using BonefireCRM.Infrastructure.Security;
using BonefireCRM.Integration.Tests.Common;
using BonefireCRM.Integration.Tests.DataSeeders;
using FastEndpoints.Testing;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Respawn;
using Testcontainers.MsSql;

[assembly: AssemblyFixture(typeof(ApiTestFixture))]

namespace BonefireCRM.Integration.Tests.Common
{
    public class ApiTestFixture : AppFixture<IApiAssemblyMarker>
    {
        private MsSqlContainer _msSqlContainer = default!;
        private Respawner _respawner = default!;

        protected override async ValueTask PreSetupAsync()
        {
            _msSqlContainer = new MsSqlBuilder()
                .Build();

            await _msSqlContainer.StartAsync();
        }

        protected override async ValueTask SetupAsync()
        {
            Client.DefaultRequestHeaders.Add("Authorization", TestConstants.AUTHSCHEMA);

            _respawner = await Respawner.CreateAsync(_msSqlContainer.GetConnectionString(), new RespawnerOptions
            {
                DbAdapter = DbAdapter.SqlServer,
                TablesToIgnore = ["__EFMigrationsHistory"],
                WithReseed = true
            });
        }

        protected override void ConfigureApp(IWebHostBuilder builder)
        {
            builder.UseEnvironment("Development");
        }

        protected override void ConfigureServices(IServiceCollection services)
        {
            services.Remove<DbContextOptions<CRMContext>>();
            services.AddDbContext<CRMContext>(options =>
            {
                options.UseSqlServer(_msSqlContainer.GetConnectionString());
            });

            services.Remove<DbContextOptions<AppDbContext>>();
            services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(_msSqlContainer.GetConnectionString());
            });

            services.AddAuthentication()
                    .AddScheme<AuthenticationSchemeOptions, TestAuthHandler>(TestConstants.AUTHSCHEMA, options => { });
            services.PostConfigure<AuthenticationOptions>(options =>
            {
                options.DefaultAuthenticateScheme = TestConstants.AUTHSCHEMA;
            });
        }

        protected override async ValueTask TearDownAsync()
        {
            //await _msSqlContainer.DisposeAsync();
            //NOTE: there's no need to dispose the container here as it will be automatically disposed by testcontainers pkg when the test run finishes.
            //      this is especially true if this AppFixture is used by many test-classes with WAF caching enabled.
            //      so, in general - containers don't need to be explicitly disposed, unless you disable WAF caching.
        }

        public async Task ResetDatabaseAsync()
        {
            await _respawner.ResetAsync(_msSqlContainer.GetConnectionString());
        }

        public async Task SeedTestDatabaseAsync(Func<CRMContext, Task> dataSeeder)
        {
            var scopeFactory = Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var crmContext = scope.ServiceProvider.GetRequiredService<CRMContext>();
                var appDbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                crmContext.SeedDefaultData();

                await AppUserTestsDataSeeder.PopulateWithTestDataAsync(appDbContext);

                await dataSeeder(crmContext);
            }
        }

        public async Task<T> ExecuteScopedDbContextAsync<T>(Func<CRMContext, Task<T>> func)
        {
            var scopeFactory = Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = scopeFactory.CreateScope())
            {
                var crmContext = scope.ServiceProvider.GetRequiredService<CRMContext>();

                var result = await func(crmContext);
                return result;
            }
        }
    }
}
