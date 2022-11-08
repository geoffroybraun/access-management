using GB.AccessManagement.Accesses.Contracts.Providers;
using GB.AccessManagement.Core.Queries;
using GB.AccessManagement.Core.ValueTypes;

namespace GB.AccessManagement.Companies.Queries.CompanyMembers;

public sealed class CompanyMembersQueryHandler : QueryHandler<CompanyMembersQuery, UserId[]>
{
    private const string ObjectType = "companies";
    private const string Relation = "member";
    private readonly IRecursiveUserIdProvider provider;

    public CompanyMembersQueryHandler(IRecursiveUserIdProvider provider)
    {
        this.provider = provider;
    }

    protected override async Task<UserId[]> Handle(CompanyMembersQuery query)
    {
        return await this.provider.Expand(ObjectType, query.CompanyId.ToString(), Relation);
    }
}