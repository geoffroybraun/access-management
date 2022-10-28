using Asp.Versioning.Builder;
using GB.AccessManagement.Accesses.Application.DeleteUserAccess;
using Microsoft.AspNetCore.Mvc;

namespace GB.AccessManagement.WebApi.Endpoints.Accesses.DeleteUserAccess;

public sealed class DeleteUserAccessEndpointDescriptor : IEndpointDescriptor
{
    private const string Endpoint = "/v{version:apiVersion}/users/{id}/accesses/{object-type}/{object-id}/{relation}";
    
    public void Describe(IEndpointRouteBuilder builder, ApiVersionSet apiVersions)
    {
        builder.MapDelete(Endpoint, async (
            [FromRoute(Name = "id")] string userId,
            [FromRoute(Name = "object-type")] string objectType,
            [FromRoute(Name = "object-id")] string objectId,
            [FromRoute(Name = "relation")] string relation,
            [FromServices] IEndpoint<DeleteUserAccessCommand> endpoint) =>
            {
                var command = new DeleteUserAccessCommand(userId, objectType, objectId, relation);

                return await endpoint.Handle(command);
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status204NoContent)
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