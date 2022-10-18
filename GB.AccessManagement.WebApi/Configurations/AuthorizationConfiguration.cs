namespace GB.AccessManagement.WebApi.Configurations;

public sealed class AuthorizationConfiguration : IWebApiConfiguration
{
    public void Configure(IServiceCollection services)
    {
        _ = services.AddAuthorization();
    }
}