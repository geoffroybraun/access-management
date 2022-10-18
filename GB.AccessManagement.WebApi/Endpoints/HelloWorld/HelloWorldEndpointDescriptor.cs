using Asp.Versioning.Builder;
using GB.AccessManagement.WebApi.Authorization;
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
            .RequireAuthorization(UserAccessAuthorizationPolicyBuilder.Build("sample", "hello-world", "access"))
            .WithName("HelloWorld")
            .WithTags("Hello world")
            .WithApiVersionSet(apiVersions)
            .MapToApiVersion(new(1, 0));
    }
}