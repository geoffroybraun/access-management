using GB.AccessManagement.WebApi.Authentication;

namespace GB.AccessManagement.WebApi.Configurations;

public sealed class AuthenticationConfiguration : IWebApiConfiguration
{
    public void Configure(IServiceCollection services)
    {
        _ = services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = DummyAuthenticationHandler.AuthenticationScheme;
                options.DefaultChallengeScheme = DummyAuthenticationHandler.AuthenticationScheme;
                options.DefaultScheme = DummyAuthenticationHandler.AuthenticationScheme;
            })
            .AddScheme<DummyAuthenticationOptions, DummyAuthenticationHandler>(
                DummyAuthenticationHandler.AuthenticationScheme,
                _ => { });
    }
}