using Asp.Versioning.Builder;
using GB.AccessManagement.Companies.Contracts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace GB.AccessManagement.WebApi.Endpoints.Companies.AddMember;

public sealed class AddMemberEndpointDescriptor : IEndpointDescriptor
{
    private const string Endpoint = "/v{version:apiVersion}/companies/{id}/members";
    
    public void Describe(IEndpointRouteBuilder builder, ApiVersionSet apiVersions)
    {
        builder.MapPost(Endpoint, async (
            [FromRoute(Name = "id")] Guid companyId,
            AddMemberRequest request,
            [FromServices] IEndpoint<AddMemberCommand> endpoint) =>
            {
                AddMemberCommand command = new(companyId, request.MemberId);

                return await endpoint.Handle(command);
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status202Accepted)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithName("AddCompanyMember")
            .WithTags("Companies")
            .WithApiVersionSet(apiVersions)
            .MapToApiVersion(new(1, 0));
    }
}