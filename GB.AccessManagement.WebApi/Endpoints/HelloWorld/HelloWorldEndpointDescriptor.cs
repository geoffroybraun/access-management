using Microsoft.AspNetCore.Mvc;

namespace GB.AccessManagement.WebApi.Endpoints.HelloWorld;

public sealed class HelloWorldEndpointDescriptor : IEndpointDescriptor
{
    public void Describe(IEndpointRouteBuilder builder)
    {
        builder
            .MapGet("/", async (
                [FromQuery(Name = "value")] string? valueToDisplay,
                [FromServices] IEndpoint<HelloWorldRequest> endpoint) =>
                {
                    var request = new HelloWorldRequest(valueToDisplay);

                    return await endpoint.Handle(request);
                })
            .WithName("HelloWorld")
            .WithTags("Hello world");
    }
}