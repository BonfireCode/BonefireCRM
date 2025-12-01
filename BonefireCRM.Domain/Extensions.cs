using BonefireCRM.Domain.Services;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static TBuilder AddDomainDependencies<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            builder.Services.AddScoped<ContactService>();
            builder.Services.AddScoped<CompanyService>();
            builder.Services.AddScoped<ActivityService>();
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<LifecycleStageService>();
            builder.Services.AddScoped<DealParticipantRoleService>();
            builder.Services.AddScoped<PipelineService>();
            builder.Services.AddScoped<PipelineStageService>();
            builder.Services.AddScoped<DealService>();
            builder.Services.AddScoped<SecurityService>();
            builder.Services.AddScoped<SeedUserDataService>();

            return builder;
        }
    }
}
