using GB.AccessManagement.Companies.Contracts.Queries;
using MediatR;

namespace GB.AccessManagement.WebApi.Endpoints.Companies.UserCompanies;

public sealed class UserCompaniesEndpoint : IEndpoint<UserCompaniesQuery>
{
    private readonly IMediator mediator;

    public UserCompaniesEndpoint(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task<IResult> Handle(UserCompaniesQuery request)
    {
        var companies = await this.mediator.Send(request);
        
        return Results.Ok(companies);
    }
}