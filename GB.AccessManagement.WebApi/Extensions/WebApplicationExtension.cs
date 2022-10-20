using System.Reflection;
using GB.AccessManagement.WebApi.Configurations;
using GB.AccessManagement.WebApi.Configurations.MiddlewareConfigurations;

namespace GB.AccessManagement.WebApi.Extensions;

public static class WebApplicationExtension
{
    public static WebApplication UseProblemDetails(this WebApplication app)
    {
        return app.Use<ProblemDetailsConfiguration>();
    }
    
    public static WebApplication UseAccesses(this WebApplication app)
    {
        return app.Use<AccessesConfiguration>();
    }
    
    public static WebApplication UseAuthentication(this WebApplication app)
    {
        return app.Use<AuthenticationConfiguration>();
    }
    
    public static WebApplication UseAuthorization(this WebApplication app)
    {
        return app.Use<AuthorizationConfiguration>();
    }
    
    public static WebApplication UseSwagger(this WebApplication app)
    {
        return app.Use<SwaggerConfiguration>();
    }
    
    public static WebApplication UseEndpoints(this WebApplication app, params Assembly[] assemblies)
    {
        return app.Use(new EndpointsConfiguration(assemblies));
    }
    
    private static WebApplication Use<TConfiguration>(this WebApplication app)
        where TConfiguration : IMiddlewareConfiguration, new()
    {
        return app.Use(new TConfiguration());
    }
    
    private static WebApplication Use<TConfiguration>(this WebApplication app, TConfiguration configuration)
        where TConfiguration : IMiddlewareConfiguration
    {
        configuration.Use(app);

        return app;
    }
}