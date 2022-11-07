using System.Reflection;
using GB.AccessManagement.Accesses.Infrastructure.Properties;
using GB.AccessManagement.Companies.Infrastructure.Properties;
using GB.AccessManagement.Core.Services;
using Scrutor;

namespace GB.AccessManagement.WebApi.Configurations.ServicesConfigurations;

public sealed class ScrutorConfiguration : IServicesConfiguration
{
    private static readonly Assembly[] Assemblies =
    {
        AccessesInfrastructureAssemblyInfo.Assembly,
        CompaniesInfrastructureAssemblyInfo.Assembly,
        typeof(Startup).Assembly
    };
    
    public void ConfigureServices(IServiceCollection services)
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