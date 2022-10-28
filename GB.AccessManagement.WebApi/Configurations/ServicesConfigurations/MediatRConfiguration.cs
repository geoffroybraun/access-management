using System.Reflection;
using GB.AccessManagement.Accesses.Application.Properties;
using MediatR;

namespace GB.AccessManagement.WebApi.Configurations.ServicesConfigurations;

public sealed class MediatRConfiguration : IServicesConfiguration
{
    private static readonly Assembly[] Assemblies =
    {
        AccessesApplicationAssemblyInfo.Assembly
    };
    
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services.AddMediatR(Assemblies);
    }
}