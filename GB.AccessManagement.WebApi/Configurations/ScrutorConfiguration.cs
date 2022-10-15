using GB.AccessManagement.WebApi.Endpoints;

namespace GB.AccessManagement.WebApi.Configurations;

public sealed class ScrutorConfiguration : IWebApiConfiguration
{
    public void Configure(IServiceCollection services)
    {
        _ = services.Scan(selector =>
        {
            selector
                .FromAssemblies(typeof(IWebApiConfiguration).Assembly)
                .AddClasses(classes => classes.AssignableTo(typeof(IEndpoint<>)))
                .AsImplementedInterfaces()
                .WithScopedLifetime();
        });
    }
}