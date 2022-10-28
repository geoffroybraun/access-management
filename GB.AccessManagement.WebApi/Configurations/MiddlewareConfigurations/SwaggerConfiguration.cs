namespace GB.AccessManagement.WebApi.Configurations.MiddlewareConfigurations;

public sealed class SwaggerConfiguration : IMiddlewareConfiguration
{
    public void Use(WebApplication app)
    {
        _ = app
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                app
                    .DescribeApiVersions()
                    .OrderByDescending(description => description.ApiVersion)
                    .ToList()
                    .ForEach(description =>
                    {
                        var url = $"/swagger/{description.GroupName}/swagger.json";
                        var name = description.GroupName.ToUpperInvariant();
                        options.SwaggerEndpoint(url, name);
                    });
            });
    }
}