namespace GB.AccessManagement.WebApi.Extensions;

public static partial class WebApplicationExtension
{
    public static WebApplication MapSwagger(this WebApplication app)
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

        return app;
    }
}