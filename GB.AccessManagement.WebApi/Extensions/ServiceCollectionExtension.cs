using GB.AccessManagement.WebApi.Configurations;
using GB.AccessManagement.WebApi.Configurations.ServicesConfigurations;
using GB.AccessManagement.WebApi.Configurations.ServicesConfigurations.Swagger;

namespace GB.AccessManagement.WebApi.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection ConfigureAuthentication(this IServiceCollection services)
    {
        return services.ConfigureServices<AuthenticationConfiguration>();
    }
    
    public static IServiceCollection ConfigureAuthorization(this IServiceCollection services)
    {
        return services.ConfigureServices<AuthorizationConfiguration>();
    }

    public static IServiceCollection ConfigureControllers(this IServiceCollection services)
    {
        return services.ConfigureServices<ControllersConfiguration>();
    }

    public static IServiceCollection ConfigureDatabase(this IServiceCollection services)
    {
        return services.ConfigureServices<DatabaseConfiguration>();
    }
    
    public static IServiceCollection ConfigureHttpClients(this IServiceCollection services)
    {
        return services.ConfigureServices<HttpClientConfiguration>();
    }

    public static IServiceCollection ConfigureMediatR(this IServiceCollection services)
    {
        return services.ConfigureServices<MediatRConfiguration>();
    }
    
    public static IServiceCollection ConfigureProblemDetails(this IServiceCollection services)
    {
        return services.ConfigureServices<ProblemDetailsConfiguration>();
    }
    
    public static IServiceCollection ConfigureScrutor(this IServiceCollection services)
    {
        return services.ConfigureServices<ScrutorConfiguration>();
    }
    
    public static IServiceCollection ConfigureSwagger(this IServiceCollection services)
    {
        return services.ConfigureServices<SwaggerConfiguration>();
    }
    
    public static IServiceCollection ConfigureVersioning(this IServiceCollection services)
    {
        return services.ConfigureServices<VersioningConfiguration>();
    }
    
    private static IServiceCollection ConfigureServices<TConfiguration>(this IServiceCollection services)
        where TConfiguration : IServicesConfiguration, new()
    {
        new TConfiguration().ConfigureServices(services);

        return services;
    }
}