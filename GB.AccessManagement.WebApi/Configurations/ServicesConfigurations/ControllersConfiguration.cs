namespace GB.AccessManagement.WebApi.Configurations.ServicesConfigurations;

public sealed class ControllersConfiguration : IServicesConfiguration
{
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services
            .AddControllers();
    }
}