namespace GB.AccessManagement.WebApi.Configurations.MiddlewareConfigurations;

public sealed class AuthenticationConfiguration : IMiddlewareConfiguration
{
    public void Use(WebApplication app)
    {
        _ = app.UseAuthentication();
    }
}