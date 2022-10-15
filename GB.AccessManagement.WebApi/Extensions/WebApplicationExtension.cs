using System.Reflection;
using GB.AccessManagement.WebApi.Endpoints;

namespace GB.AccessManagement.WebApi.Extensions;

public static class WebApplicationExtension
{
    public static void MapEndpointDescriptors(this WebApplication app, params Assembly[] assemblies)
    {
        assemblies
            .SelectMany(FilterEndpointDesciptors)
            .Distinct()
            .ToList()
            .ForEach(descriptor => descriptor.Describe(app));
    }

    private static IEndpointDescriptor[] FilterEndpointDesciptors(Assembly assembly)
    {
        return assembly
            .DefinedTypes
            .Where(IsTypeEndpointDesciptor)
            .Select(Activator.CreateInstance)
            .Cast<IEndpointDescriptor>()
            .ToArray();
    }

    private static bool IsTypeEndpointDesciptor(Type type)
    {
        return !type.IsInterface
               && !type.IsAbstract
               && type.IsAssignableTo(typeof(IEndpointDescriptor));
    }
}