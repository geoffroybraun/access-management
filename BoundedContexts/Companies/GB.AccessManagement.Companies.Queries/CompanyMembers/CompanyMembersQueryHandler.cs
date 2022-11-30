using GB.AccessManagement.Accesses.Contracts.Queries;
using GB.AccessManagement.Companies.Contracts.Queries;
using GB.AccessManagement.Core.Queries;
using MediatR;

namespace GB.AccessManagement.Companies.Queries.CompanyMembers;

public sealed class CompanyMembersQueryHandler : QueryHandler<CompanyMembersQuery, Guid[]>
{
    private const string ObjectType = "companies";
    private const string Relation = "member";
    private readonly IMediator mediator;

    public CompanyMembersQueryHandler(IMediator mediator)
    {
        this.mediator = mediator;
    }

    protected override async Task<Guid[]> Handle(CompanyMembersQuery query)
    {
        var userIdsQuery = new ObjectUserIdsQuery(ObjectType, query.CompanyId.ToString(), Relation, true);
        var userIds = await this.mediator.Send(userIdsQuery);

        return userIds
            .Where(userId => !userId.Contains(':'))
            .Select(userId => Guid.Parse(userId))
            .ToArray();
    }
}