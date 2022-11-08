using GB.AccessManagement.Accesses.Contracts.Providers;
using GB.AccessManagement.Core.Queries;
using GB.AccessManagement.Core.ValueTypes;

namespace GB.AccessManagement.Companies.Queries.GetCompanyOwner;

public sealed class CompanyOwnerQueryHandler : QueryHandler<CompanyOwnerQuery, UserId>
{
    private const string ObjectType = "companies";
    private const string Relation = "owner";
    private readonly IUserIdProvider provider;

    public CompanyOwnerQueryHandler(IUserIdProvider provider)
    {
        this.provider = provider;
    }

    protected override async Task<UserId> Handle(CompanyOwnerQuery query)
    {
        var userIds = await this.provider.List(ObjectType, query.CompanyId.ToString(), Relation);

        return userIds.Single();
    }
}