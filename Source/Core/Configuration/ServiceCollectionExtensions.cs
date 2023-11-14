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

        foreach (var mapping in LocalizationMap
                     .Concat(ValidationMap)
                     .Concat(ServiceMap)
                     .Concat(JSModuleMap))
        {
            serviceCollection.AddScoped(mapping.Key, mapping.Value);
        }

        return serviceCollection;
    }

    public static IServiceCollection AddClassProvider(this IServiceCollection serviceCollection,
        Func<IClassProvider> classProviderFactory)
    {
        serviceCollection.AddSingleton((_) => classProviderFactory());

        return serviceCollection;
    }

    //public static IServiceCollection AddStyleProvider(this IServiceCollection serviceCollection, Func<IStyleProvider> styleProviderFactory)
    //{
    //    serviceCollection.AddSingleton((_) => styleProviderFactory());

    //    return serviceCollection;
    //}

    //public static IServiceCollection AddIconProvider(this IServiceCollection serviceCollection, Func<IIconProvider> iconProviderFactory)
    //{
    //    serviceCollection.AddSingleton((_) => iconProviderFactory());

    //    return serviceCollection;
    //}

    private static IDictionary<Type, Type> LocalizationMap => new Dictionary<Type, Type>
    {
    };

    public static IDictionary<Type, Type> ValidationMap => new Dictionary<Type, Type>
    {
    };

    public static IDictionary<Type, Type> ServiceMap => new Dictionary<Type, Type>
    {
    };

    public static IDictionary<Type, Type> JSModuleMap => new Dictionary<Type, Type>
    {
        //{ typeof( IJsUtilitiesModule ), typeof( JsUtilitiesModule ) },
    };
}
