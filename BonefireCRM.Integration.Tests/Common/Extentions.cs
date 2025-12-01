using Microsoft.Extensions.DependencyInjection;

namespace BonefireCRM.Integration.Tests.Common
{
    internal static class Extentions
    {
        internal static void Remove<T>(this IServiceCollection services)
        {
            var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType == typeof(T));
            if (dbContextDescriptor is not null)
            {
                services.Remove(dbContextDescriptor);
            }
        }

        internal static void RemoveAll<T>(this IServiceCollection services)
        {
            var dbContextDescriptors = services.Where(d => d.ServiceType == typeof(T)).ToList();
            foreach (var dbContextDescriptor in dbContextDescriptors)
            {
                services.Remove(dbContextDescriptor);
            }
        }
    }
}
