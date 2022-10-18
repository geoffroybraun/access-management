using Asp.Versioning.ApiExplorer;
using GB.AccessManagement.WebApi.Authentication;
using GB.AccessManagement.WebApi.Services;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
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
            options.AddSecurityDefinition(DummyAuthenticationHandler.AuthenticationScheme,
                new()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = DummyAuthenticationHandler.AuthenticationScheme,
                    In = ParameterLocation.Header
                });
            options.AddSecurityRequirement(new()
            {
                {
                    new()
                    {
                        Reference = new()
                        {
                            Id = DummyAuthenticationHandler.AuthenticationScheme,
                            Type = ReferenceType.SecurityScheme
                        }
                    },
                    Array.Empty<string>()
                }
            });
        }
    }
}