using System.Reflection;
using GB.AccessManagement.WebApi.Extensions;

namespace GB.AccessManagement.WebApi;

public sealed class Startup
{
    private static readonly Assembly[] Assemblies =
    {
        typeof(Startup).Assembly
    };
    
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services
            .ConfigureAuthentication()
            .ConfigureAuthorization()
            .ConfigureDatabase()
            .ConfigureHttpClients()
            .ConfigureMediatR()
            .ConfigureProblemDetails()
            .ConfigureScrutor()
            .ConfigureSwagger()
            .ConfigureVersioning();
    }

    public void Configure(WebApplication app)
    {
        _ = app
            .UseProblemDetails()
            .UseAccesses()
            .UseAuthentication()
            .UseAuthorization()
            .UseSwagger()
            .UseEndpoints(Assemblies);
    }
}