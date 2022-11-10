using Asp.Versioning.Builder;
using GB.AccessManagement.Companies.Contracts.Presentations;
using GB.AccessManagement.Companies.Contracts.Queries;
using Microsoft.AspNetCore.Mvc;

namespace GB.AccessManagement.WebApi.Endpoints.Companies.UserCompanies;

public sealed class UserCompaniesEndpointDescriptor : IEndpointDescriptor
{
    private const string Endpoint = "/v{version:apiVersion}/users/{id}/companies";
    
    public void Describe(IEndpointRouteBuilder builder, ApiVersionSet apiVersions)
    {
        builder.MapGet(Endpoint, async (
            [FromRoute(Name = "id")] Guid userId,
            [FromServices] IEndpoint<UserCompaniesQuery> endpoint) =>
            {
                UserCompaniesQuery query = new(userId);

                return await endpoint.Handle(query);
            })
            .RequireAuthorization()
            .Produces<CompanyPresentation[]>()
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithName("UserCompanies")
            .WithTags("Companies")
            .WithApiVersionSet(apiVersions)
            .MapToApiVersion(new(1, 0));
    }
}