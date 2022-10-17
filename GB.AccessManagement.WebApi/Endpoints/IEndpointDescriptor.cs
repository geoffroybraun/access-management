using Asp.Versioning.Builder;

namespace GB.AccessManagement.WebApi.Endpoints;

public interface IEndpointDescriptor
{
    void Describe(IEndpointRouteBuilder builder, ApiVersionSet apiVersions);
}