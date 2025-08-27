using BonefireCRM.Domain.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BonefireCRM.Domain
{
    public static class Extensions
    {
        public static TBuilder AddDomainDependencies<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            builder.Services.AddScoped<ServiceContact>();

            return builder;
        }
    }
}
