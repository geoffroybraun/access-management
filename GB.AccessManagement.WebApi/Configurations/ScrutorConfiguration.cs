using System.Reflection;
using GB.AccessManagement.Accesses.Infrastructure.Properties;
using GB.AccessManagement.Core.Services;
using Scrutor;

namespace GB.AccessManagement.WebApi.Configurations;

public sealed class ScrutorConfiguration : IWebApiConfiguration
{
    private static readonly Assembly[] Assemblies =
    {
        AccessesInfrastructureAssemblyInfo.Assembly,
        typeof(Startup).Assembly
    };
    
    public void Configure(IServiceCollection services)
    {
        _ = services.Scan(selector =>
        {
            var assembliesSelector = selector.FromAssemblies(Assemblies);
            AddTransientClasses(assembliesSelector);
            AddScopedClasses(assembliesSelector);
            AddSingletonClasses(assembliesSelector);
        });
    }

    private static void AddTransientClasses(IImplementationTypeSelector selector)
    {
        _ = selector
            .AddClasses(classes => classes.AssignableTo<ITransientService>())
            .AsImplementedInterfaces()
            .WithTransientLifetime()
            .AddClasses(classes => classes.AssignableTo<ISelfTransientService>())
            .AsSelf()
            .WithTransientLifetime();
    }

    private static void AddScopedClasses(IImplementationTypeSelector selector)
    {
        _ = selector
            .AddClasses(classes => classes.AssignableTo<IScopedService>())
            .AsImplementedInterfaces()
            .WithScopedLifetime();
    }

    private static void AddSingletonClasses(IImplementationTypeSelector selector)
    {
        _ = selector
            .AddClasses(classes => classes.AssignableTo<ISingletonService>())
            .AsImplementedInterfaces()
            .WithSingletonLifetime();
    }
}