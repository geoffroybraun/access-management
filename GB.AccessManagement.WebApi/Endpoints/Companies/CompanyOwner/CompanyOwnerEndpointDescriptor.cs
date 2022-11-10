using Asp.Versioning.Builder;
using GB.AccessManagement.Companies.Contracts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace GB.AccessManagement.WebApi.Endpoints.Companies.CompanyOwner;

public sealed class CompanyOwnerEndpointDescriptor : IEndpointDescriptor
{
    private const string Endpoint = "/v{version:apiVersion}/companies/{id}/owner";
    
    public void Describe(IEndpointRouteBuilder builder, ApiVersionSet apiVersions)
    {
        builder.MapGet(Endpoint, async (
                [FromRoute(Name = "id")] Guid companyId,
                [FromServices] IEndpoint<CompanyOwnerQuery> endpoint) =>
            {
                var query = new CompanyOwnerQuery(companyId);

                return await endpoint.Handle(query);
            })
            .RequireAuthorization()
            .Produces<string>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithName("CompanyOwner")
            .WithTags("Companies")
            .WithApiVersionSet(apiVersions)
            .MapToApiVersion(new(1, 0));
    }
}