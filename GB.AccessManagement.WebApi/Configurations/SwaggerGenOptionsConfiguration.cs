using Asp.Versioning.ApiExplorer;
using GB.AccessManagement.WebApi.Services;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GB.AccessManagement.WebApi.Configurations;

public sealed class SwaggerGenOptionsConfiguration : IConfigureOptions<SwaggerGenOptions>, ITransientService
{
    private readonly IApiVersionDescriptionProvider provider;

    public SwaggerGenOptionsConfiguration(IApiVersionDescriptionProvider provider)
    {
        this.provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (ApiVersionDescription description in this.provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, new()
            {
                Title = ".Net Access Management",
                Version = description.ApiVersion.ToString(),
                Contact = new()
                {
                    Name = "Geoffroy BRAUN",
                    Url = new("https://github.com/geoffroybraun/")
                },
                License = new()
                {
                    Name = "Geoffroy BRAUN",
                    Url = new("https://github.com/geoffroybraun/")
                }
            });
        }
    }
}