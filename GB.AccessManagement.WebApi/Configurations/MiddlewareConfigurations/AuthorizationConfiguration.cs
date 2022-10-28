namespace GB.AccessManagement.WebApi.Configurations.MiddlewareConfigurations;

public sealed class AuthorizationConfiguration : IMiddlewareConfiguration
{
    public void Use(WebApplication app)
    {
        _ = app.UseAuthorization();
    }
}