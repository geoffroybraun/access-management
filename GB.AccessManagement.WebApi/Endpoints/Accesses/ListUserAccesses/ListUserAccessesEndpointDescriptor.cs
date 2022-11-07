using Asp.Versioning.Builder;
using GB.AccessManagement.Accesses.Contracts.ValueTypes;
using GB.AccessManagement.Accesses.Queries.ListUserAccesses;
using Microsoft.AspNetCore.Mvc;

namespace GB.AccessManagement.WebApi.Endpoints.Accesses.ListUserAccesses;

public sealed class ListUserAccessesEndpointDescriptor : IEndpointDescriptor
{
    private const string Endpoint = "/v{version:apiVersion}/users/{id}/accesses/{object-type}";
    
    public void Describe(IEndpointRouteBuilder builder, ApiVersionSet apiVersions)
    {
        builder.MapGet(Endpoint, async (
            [FromRoute(Name = "id")] string userId,
            [FromRoute(Name = "object-type")] string objectType,
            [FromServices] IEndpoint<ListUserAccessesQuery> endpoint) =>
            {
                ListUserAccessesQuery query = new(userId, objectType);

                return await endpoint.Handle(query);
            })
            .RequireAuthorization()
            .Produces<UserAccess[]>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithName("ListUserAccesses")
            .WithTags("Accesses")
            .WithApiVersionSet(apiVersions)
            .MapToApiVersion(new(1, 0));
    }
}