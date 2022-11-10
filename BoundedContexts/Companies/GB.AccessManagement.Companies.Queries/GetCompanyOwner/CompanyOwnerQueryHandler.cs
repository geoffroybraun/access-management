using GB.AccessManagement.Accesses.Contracts.Queries;
using GB.AccessManagement.Companies.Contracts.Queries;
using GB.AccessManagement.Core.Queries;
using MediatR;

namespace GB.AccessManagement.Companies.Queries.GetCompanyOwner;

public sealed class CompanyOwnerQueryHandler : QueryHandler<CompanyOwnerQuery, Guid>
{
    private const string ObjectType = "companies";
    private const string Relation = "owner";
    private readonly IMediator _mediator;

    public CompanyOwnerQueryHandler(IMediator mediator)
    {
        _mediator = mediator;
    }

    protected override async Task<Guid> Handle(CompanyOwnerQuery query)
    {
        var membersQuery = new ListObjectUserIdsQuery(ObjectType, query.CompanyId.ToString(), Relation);
        var userIds = await this._mediator.Send(membersQuery);

        return Guid.Parse(userIds.Single());
    }
}