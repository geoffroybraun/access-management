using System.Reflection;
using GB.AccessManagement.WebApi.Endpoints;

namespace GB.AccessManagement.WebApi.Extensions;

public static partial class WebApplicationExtension
{
    public static WebApplication MapEndpointDescriptors(this WebApplication app, params Assembly[] assemblies)
    {
        var apiVersions = app
            .NewApiVersionSet()
            .HasApiVersion(new(1, 0))
            .Build();
        
        assemblies
            .SelectMany(FilterEndpointDesciptors)
            .Distinct()
            .ToList()
            .ForEach(descriptor => descriptor.Describe(app, apiVersions));

        return app;
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