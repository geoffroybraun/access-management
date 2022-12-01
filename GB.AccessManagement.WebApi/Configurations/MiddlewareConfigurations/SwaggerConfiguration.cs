using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace GB.AccessManagement.WebApi.Configurations.MiddlewareConfigurations;

public sealed class SwaggerConfiguration : IMiddlewareConfiguration
{
    public void Use(IApplicationBuilder app)
    {
        _ = app
            .UseSwagger()
            .UseSwaggerUI(options =>
            {
                var descriptions = app
                    .ApplicationServices
                    .GetRequiredService<IApiVersionDescriptionProvider>()
                    .ApiVersionDescriptions
                    .OrderByDescending(description => description.ApiVersion)
                    .ToList();
                
                descriptions
                    .ForEach(description =>
                    {
                        var url = $"/swagger/{description.GroupName}/swagger.json";
                        var name = description.GroupName.ToUpperInvariant();
                        options.SwaggerEndpoint(url, name);
                    });
            });
    }
}