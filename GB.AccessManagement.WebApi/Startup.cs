using GB.AccessManagement.WebApi.Extensions;
using Hellang.Middleware.ProblemDetails;

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

    public void Configure(IApplicationBuilder app)
    {
        _ = app
            .UseProblemDetails()
            .UseAccesses()
            .UseRouting()
            .UseAuthentication()
            .UseAuthorization()
            .UseEndpoints(endpoints => _ = endpoints.MapControllers())
            .UseSwagger();
    }
}