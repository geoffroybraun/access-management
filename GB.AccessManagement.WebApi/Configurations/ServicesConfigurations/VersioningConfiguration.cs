namespace GB.AccessManagement.WebApi.Configurations.ServicesConfigurations;

public sealed class VersioningConfiguration : IServicesConfiguration
{
    public void ConfigureServices(IServiceCollection services)
    {
        _ = services
            .AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.DefaultApiVersion = new(1, 0);
                options.AssumeDefaultVersionWhenUnspecified = true;
            })
            .AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });
    }
}