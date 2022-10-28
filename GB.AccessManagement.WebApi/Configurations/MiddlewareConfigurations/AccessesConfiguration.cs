using GB.AccessManagement.Accesses.Infrastructure;
using GB.AccessManagement.Accesses.Infrastructure.Middlewares;
using Microsoft.Extensions.Options;

namespace GB.AccessManagement.WebApi.Configurations.MiddlewareConfigurations;

public sealed class AccessesConfiguration : IMiddlewareConfiguration
{
    public void Use(WebApplication app)
    {
        _ = app.UseWhen(AreOptionsNotValid, UseOpenFga);
    }

    private static bool AreOptionsNotValid(HttpContext context)
    {
        return !AreOptionsValid(context);
    }

    private static bool AreOptionsValid(HttpContext context)
    {
        OpenFgaOptions options = context
            .RequestServices
            .GetRequiredService<IOptions<OpenFgaOptions>>()
            .Value;

        return options.IsValid();
    }

    private static void UseOpenFga(IApplicationBuilder builder)
    {
        _ = builder.UseMiddleware<OpenFgaMiddleware>();
    }
}