namespace GB.AccessManagement.WebApi.Configurations.ServicesConfigurations;

public sealed class VersioningConfiguration : IServicesConfiguration
{
    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddEndpointsApiExplorer()
            .AddApiVersioning(options => options.ReportApiVersions = true)
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}