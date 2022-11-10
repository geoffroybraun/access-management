using GB.AccessManagement.Companies.Contracts.Queries;
using MediatR;

namespace GB.AccessManagement.WebApi.Endpoints.Companies.CompanyMembers;

public sealed class CompanyMembersEndpoint : IEndpoint<CompanyMembersQuery>
{
    private readonly IMediator mediator;

    public CompanyMembersEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IResult> Handle(CompanyMembersQuery request)
    {
        var memberIds = await this.mediator.Send(request);

        return Results.Ok(memberIds.Select(id => id.ToString()).ToArray());
    }
}