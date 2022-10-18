using Asp.Versioning.Builder;
using GB.AccessManagement.WebApi.Configurations.Constants;
using Microsoft.AspNetCore.Mvc;

namespace GB.AccessManagement.WebApi.Endpoints.HelloWorld;

public sealed class HelloWorldEndpointDescriptor : IEndpointDescriptor
{
    public void Describe(IEndpointRouteBuilder builder, ApiVersionSet apiVersions)
    {
        builder
            .MapGet("/v{version:apiVersion}", async (
                [FromQuery(Name = "value")] string? valueToDisplay,
                [FromServices] IEndpoint<HelloWorldRequest> endpoint) =>
                {
                    var request = new HelloWorldRequest(valueToDisplay);

                    return await endpoint.Handle(request);
                })
            .RequireAuthorization()
            .WithName("HelloWorld")
            .WithTags("Hello world")
            .WithApiVersionSet(apiVersions)
            .MapToApiVersion(ApiVersions.Version1_0);
    }
}