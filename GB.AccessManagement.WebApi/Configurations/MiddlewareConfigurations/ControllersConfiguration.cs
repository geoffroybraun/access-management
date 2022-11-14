namespace GB.AccessManagement.WebApi.Configurations.MiddlewareConfigurations;

public sealed class ControllersConfiguration : IMiddlewareConfiguration
{
    public void Use(WebApplication app)
    {
        _ = app.MapControllers();
    }
}