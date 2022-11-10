using Asp.Versioning.Builder;
using GB.AccessManagement.Companies.Contracts.Commands;
using Microsoft.AspNetCore.Mvc;

namespace GB.AccessManagement.WebApi.Endpoints.Companies.CreateCompany;

public sealed class CreateCompanyEndpointDescriptor : IEndpointDescriptor
{
    private const string Endpoint = "/v{version:apiVersion}/users/{id}/companies";
    
    public void Describe(IEndpointRouteBuilder builder, ApiVersionSet apiVersions)
    {
        builder.MapPost(Endpoint, async (
            [FromRoute(Name = "id")] Guid userId,
            CreateCompanyRequest request,
            [FromServices] IEndpoint<CreateCompanyCommand> endpoint) =>
            {
                var command = new CreateCompanyCommand(request.Name, userId);
                
                return await endpoint.Handle(command);
            })
            .RequireAuthorization()
            .Produces(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status401Unauthorized)
            .ProducesProblem(StatusCodes.Status403Forbidden)
            .ProducesProblem(StatusCodes.Status422UnprocessableEntity)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .WithName("CreateCompany")
            .WithTags("Companies")
            .WithApiVersionSet(apiVersions)
            .MapToApiVersion(new(1, 0));
    }
}