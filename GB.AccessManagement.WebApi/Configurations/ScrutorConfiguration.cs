using GB.AccessManagement.WebApi.Services;
using Scrutor;

namespace GB.AccessManagement.WebApi.Configurations;

public sealed class ScrutorConfiguration : IWebApiConfiguration
{
    public void Configure(IServiceCollection services)
    {
        _ = services.Scan(selector =>
        {
            var assembliesSelector = selector.FromAssemblies(typeof(IWebApiConfiguration).Assembly);
            AddTransientClasses(assembliesSelector);
            AddScopedClasses(assembliesSelector);
        });
    }

    private static void AddTransientClasses(IImplementationTypeSelector selector)
    {
        _ = selector
            .AddClasses(classes => classes.AssignableTo<ITransientService>())
            .AsImplementedInterfaces()
            .WithTransientLifetime();
    }

    private static void AddScopedClasses(IImplementationTypeSelector selector)
    {
        _ = selector
            .AddClasses(classes => classes.AssignableTo<IScopedService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime();
    }
}