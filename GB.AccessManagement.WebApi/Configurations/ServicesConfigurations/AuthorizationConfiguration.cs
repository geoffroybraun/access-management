namespace GB.AccessManagement.WebApi.Configurations.ServicesConfigurations;

public sealed class AuthorizationConfiguration : IServicesConfiguration
{
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services.AddAuthorization();
    }
}