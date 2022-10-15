namespace GB.AccessManagement.WebApi.Endpoints;

public interface IEndpointDescriptor
{
    void Describe(IEndpointRouteBuilder builder);
}