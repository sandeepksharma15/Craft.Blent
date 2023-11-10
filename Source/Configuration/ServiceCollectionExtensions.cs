using Craft.Blent.Contracts.Providers;
using Craft.Blent.Providers;
using Microsoft.Extensions.DependencyInjection;

namespace Craft.Blent.Configuration;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCraftBlent(this IServiceCollection serviceCollection,
        Action<BlentOptions>? configureOptions = null)
    {
        // If options handler is not defined we will get an exception so
        // we need to initialize an empty action.
        configureOptions ??= _ => { };

        serviceCollection.AddSingleton(configureOptions);
        serviceCollection.AddSingleton<BlentOptions>();

        serviceCollection.AddSingleton<IUniqueIdProvider, UniqueIdProvider>();

        return serviceCollection;
    }
}
