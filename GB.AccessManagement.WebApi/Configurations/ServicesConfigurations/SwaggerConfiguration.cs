namespace GB.AccessManagement.WebApi.Configurations.ServicesConfigurations;

public sealed class SwaggerConfiguration : IServicesConfiguration
{
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services.AddSwaggerGen();
    }
}