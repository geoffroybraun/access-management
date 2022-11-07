using System.Reflection;
using GB.AccessManagement.Accesses.Commands.Properties;
using GB.AccessManagement.Accesses.Queries.Properties;
using GB.AccessManagement.Companies.Commands.Properties;
using MediatR;

namespace GB.AccessManagement.WebApi.Configurations.ServicesConfigurations;

public sealed class MediatRConfiguration : IServicesConfiguration
{
    private static readonly Assembly[] Assemblies =
    {
        AccessesCommandsAssemblyInfo.Assembly,
        AccessesQueriesAssemblyInfo.Assembly,
        CompaniesCommandsAssemblyInfo.Assembly
    };
    
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services.AddMediatR(Assemblies);
    }
}