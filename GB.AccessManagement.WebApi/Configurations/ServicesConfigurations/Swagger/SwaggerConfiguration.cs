namespace GB.AccessManagement.WebApi.Configurations.ServicesConfigurations.Swagger;

public sealed class SwaggerConfiguration : IServicesConfiguration
{
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services.AddSwaggerGen();
    }
}