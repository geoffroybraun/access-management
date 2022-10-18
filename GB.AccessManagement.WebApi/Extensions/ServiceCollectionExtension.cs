using System.Reflection;
using GB.AccessManagement.WebApi.Configurations;

namespace GB.AccessManagement.WebApi.Extensions;

public static class ServiceCollectionExtension
{
    public static void Configure(this IServiceCollection services, params Assembly[] assemblies)
    {
        assemblies
            .SelectMany(FilterWebApiConfigurations)
            .ToList()
            .ForEach(configuration => configuration.Configure(services));
    }

    private static IWebApiConfiguration[] FilterWebApiConfigurations(Assembly assembly)
    {
        return assembly
            .DefinedTypes
            .Where(IsTypeWebApiConfiguration)
            .Select(Activator.CreateInstance)
            .Cast<IWebApiConfiguration>()
            .ToArray();
    }

    private static bool IsTypeWebApiConfiguration(Type type)
    {
        return !type.IsInterface
               && !type.IsAbstract
               && type.IsAssignableTo(typeof(IWebApiConfiguration));
    }
}