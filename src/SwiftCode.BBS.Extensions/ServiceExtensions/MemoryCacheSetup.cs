using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using SwiftCode.BSS.Common.MemoryCache;

namespace SwiftCode.BBS.Extensions.ServiceExtensions;

public static class MemoryCacheSetup
{
    public static void AddMemoryCacheSetup(this IServiceCollection services)
    {
        ArgumentNullException.ThrowIfNull(services);

        services.AddScoped<ICachingProvider, MemoryCachingProvider>();
        services.AddSingleton<IMemoryCache>(factory =>
        {
            var cache = new MemoryCache(new MemoryCacheOptions());
            return cache;
        });
    }
}