using GB.AccessManagement.WebApi.Extensions;

namespace GB.AccessManagement.WebApi;

public sealed class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services
            .ConfigureAuthentication()
            .ConfigureAuthorization()
            .ConfigureControllers()
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
            .UseControllers()
            .UseSwagger();
    }
}