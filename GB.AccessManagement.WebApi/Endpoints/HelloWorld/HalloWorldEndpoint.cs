namespace GB.AccessManagement.WebApi.Endpoints.HelloWorld;

public sealed class HalloWorldEndpoint : IEndpoint<HelloWorldRequest>
{
    private const string DefaultValueToDisplay = "Hello world!";

    public Task<IResult> Handle(HelloWorldRequest request)
    {
        var valueToDisplay = request.valueToDisplay ?? DefaultValueToDisplay;

        return Task.FromResult(Results.Ok(valueToDisplay));
    }
}