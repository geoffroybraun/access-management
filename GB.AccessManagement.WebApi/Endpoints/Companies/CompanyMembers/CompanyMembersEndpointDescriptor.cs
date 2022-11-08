using Asp.Versioning.Builder;
using GB.AccessManagement.Companies.Queries.CompanyMembers;
using Microsoft.AspNetCore.Mvc;

namespace GB.AccessManagement.WebApi.Endpoints.Companies.CompanyMembers;

public sealed class CompanyMembersEndpointDescriptor : IEndpointDescriptor
{
    private const string Endpoint = "/v{version:apiVersion}/companies/{id}/members";
    
    public void Describe(IEndpointRouteBuilder builder, ApiVersionSet apiVersions)
    {
        builder.MapGet(Endpoint, async (
                [FromRoute(Name = "id")] Guid companyId,
                [FromServices] IEndpoint<CompanyMembersQuery> endpoint) =>
            {
                var query = new CompanyMembersQuery(companyId);

                return await endpoint.Handle(query);
            })
            .RequireAuthorization()
            .Produces<string[]>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithName("CompanyMembers")
            .WithTags("Companies")
            .WithApiVersionSet(apiVersions)
            .MapToApiVersion(new(1, 0));
    }
}