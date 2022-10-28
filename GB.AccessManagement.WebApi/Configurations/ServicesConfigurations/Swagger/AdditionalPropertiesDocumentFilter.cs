using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace GB.AccessManagement.WebApi.Configurations.ServicesConfigurations.Swagger;

internal sealed class AdditionalPropertiesDocumentFilter : IDocumentFilter
{
    public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
    {
        swaggerDoc
            .Components
            .Schemas
            .Values
            .ToList()
            .ForEach(schema =>
            {
                schema.AdditionalProperties = null;
                schema.AdditionalPropertiesAllowed = false;
            });
    }
}