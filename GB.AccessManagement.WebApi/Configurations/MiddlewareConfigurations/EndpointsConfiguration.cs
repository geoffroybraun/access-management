using System.Reflection;
using GB.AccessManagement.WebApi.Endpoints;

namespace GB.AccessManagement.WebApi.Configurations.MiddlewareConfigurations;

public sealed class EndpointsConfiguration : IMiddlewareConfiguration
{
    private readonly Assembly[] assemblies;

    public EndpointsConfiguration(Assembly[] assemblies)
    {
        this.assemblies = assemblies;
    }

    public void Use(WebApplication app)
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