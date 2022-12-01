using GB.AccessManagement.WebApi.Configurations;
using GB.AccessManagement.WebApi.Configurations.MiddlewareConfigurations;

namespace GB.AccessManagement.WebApi.Extensions;

public static class WebApplicationExtension
{
    public static IApplicationBuilder UseAccesses(this IApplicationBuilder app)
    {
        return app.Use<AccessesConfiguration>();
    }
    
    public static IApplicationBuilder UseSwagger(this IApplicationBuilder app)
    {
        return app.Use<SwaggerConfiguration>();
    }
    
    private static IApplicationBuilder Use<TConfiguration>(this IApplicationBuilder app)
        where TConfiguration : IMiddlewareConfiguration, new()
    {
        return app.Use(new TConfiguration());
    }
    
    private static IApplicationBuilder Use<TConfiguration>(this IApplicationBuilder app, TConfiguration configuration)
        where TConfiguration : IMiddlewareConfiguration
    {
        configuration.Use(app);

        return app;
    }
}