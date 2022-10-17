namespace GB.AccessManagement.WebApi.Configurations;

public sealed class SwaggerConfiguration : IWebApiConfiguration
{
    public void Configure(IServiceCollection services)
    {
        services.AddSwaggerGen();
    }
}