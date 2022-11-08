using Asp.Versioning.Builder;
using GB.AccessManagement.Accesses.Contracts.ValueTypes;
using GB.AccessManagement.Accesses.Queries;
using GB.AccessManagement.Accesses.Queries.GetUserAccess;
using Microsoft.AspNetCore.Mvc;

namespace GB.AccessManagement.WebApi.Endpoints.Accesses.GetUserAccess;

public sealed class GetUserAccessEndpointDescriptor : IEndpointDescriptor
{
    private const string Endpoint = "/v{version:apiVersion}/users/{id}/accesses/{object-type}/{object-id}";
    
    public void Describe(IEndpointRouteBuilder builder, ApiVersionSet apiVersions)
    {
        builder.MapGet(Endpoint, async (
            [FromRoute(Name = "id")] Guid userId,
            [FromRoute(Name = "object-type")] string objectType,
            [FromRoute(Name = "object-id")] string objectId,
            [FromServices] IEndpoint<GetUserAccessQuery> endpoint) =>
            {
                GetUserAccessQuery query = new(userId, objectType, objectId);

                return await endpoint.Handle(query);
            })
            .RequireAuthorization()
            .Produces<UserAccessPresentation>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithName("GetUserAccess")
            .WithTags("Accesses")
            .WithApiVersionSet(apiVersions)
            .MapToApiVersion(new(1, 0));
    }
}