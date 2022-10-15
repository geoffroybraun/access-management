using System.Reflection;
using GB.AccessManagement.WebApi.Extensions;

namespace GB.AccessManagement.WebApi;

public sealed class Startup
{
    private static readonly Assembly TargetAssembly = typeof(Startup).Assembly;
    
    public void ConfigureServices(IServiceCollection services)
    {
        services.Configure(TargetAssembly);
    }

    public void Configure(WebApplication app)
    {
        app.MapEndpointDescriptors(TargetAssembly);
    }
}