namespace GB.AccessManagement.WebApi.Configurations;

public sealed class VersioningConfiguration : IWebApiConfiguration
{
    public void Configure(IServiceCollection services)
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