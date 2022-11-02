using Asp.Versioning.Builder;
using GB.AccessManagement.Accesses.Commands.CreateUserAccess;
using Microsoft.AspNetCore.Mvc;

namespace GB.AccessManagement.WebApi.Endpoints.Accesses.CreateUserAccess;

public sealed class CreateUserAccessEndpointDescriptor : IEndpointDescriptor
{
    private const string Endpoint = "/v{version:apiVersion}/users/{id}/accesses";
    
    public void Describe(IEndpointRouteBuilder builder, ApiVersionSet apiVersions)
    {
        builder.MapPost(Endpoint, async (
            [FromRoute(Name = "id")] string userId,
            [FromBody] CreateUserAccessRequest request,
            [FromServices] IEndpoint<CreateUserAccessCommand> endpoint) =>
            {
                return await endpoint.Handle(request.ToCommand(userId));
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithName("CreateUserAccess")
            .WithTags("Accesses")
            .WithApiVersionSet(apiVersions)
            .MapToApiVersion(new(1, 0));
    }
}