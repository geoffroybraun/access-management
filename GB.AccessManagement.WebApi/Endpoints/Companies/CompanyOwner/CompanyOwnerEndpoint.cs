using GB.AccessManagement.Companies.Contracts.Queries;
using MediatR;

namespace GB.AccessManagement.WebApi.Endpoints.Companies.CompanyOwner;

public sealed class CompanyOwnerEndpoint : IEndpoint<CompanyOwnerQuery>
{
    private readonly IMediator mediator;

    public CompanyOwnerEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IResult> Handle(CompanyOwnerQuery request)
    {
        var userId = await this.mediator.Send(request);

        return Results.Ok(userId.ToString());
    }
}