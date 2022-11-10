using Asp.Versioning.Builder;
using GB.AccessManagement.Companies.Contracts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace GB.AccessManagement.WebApi.Endpoints.Companies.RemoveMember;

public sealed class RemoveMemberEndpointDescriptor : IEndpointDescriptor
{
    private const string Endpoint = "/v{version:apiVersion}/companies/{company}/members/{member}";
    
    public void Describe(IEndpointRouteBuilder builder, ApiVersionSet apiVersions)
    {
        builder.MapDelete(Endpoint, async (
            [FromRoute(Name = "company")] Guid companyId,
            [FromRoute(Name = "member")] Guid memberId,
            [FromServices] IEndpoint<RemoveMemberCommand> endpoint) =>
            {
                var command = new RemoveMemberCommand(companyId, memberId);

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
            .WithName("RemoveCompanyMember")
            .WithTags("Companies")
            .WithApiVersionSet(apiVersions)
            .MapToApiVersion(new(1, 0));
    }
}