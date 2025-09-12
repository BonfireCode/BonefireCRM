using BonefireCRM.Domain.Services;
using Microsoft.Extensions.Hosting;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class Extensions
    {
        public static TBuilder AddDomainDependencies<TBuilder>(this TBuilder builder) where TBuilder : IHostApplicationBuilder
        {
            builder.Services.AddScoped<ContactService>();

            return builder;
        }
    }
}
