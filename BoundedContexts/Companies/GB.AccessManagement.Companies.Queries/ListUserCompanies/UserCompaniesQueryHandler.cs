using GB.AccessManagement.Accesses.Contracts.Providers;
using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Companies.Queries.ListUserCompanies;

public sealed class UserCompaniesQueryHandler : QueryHandler<UserCompaniesQuery, CompanyPresentation[]>
{
    private readonly IObjectIdProvider provider;
    private readonly ICompanyRepository repository;

    public UserCompaniesQueryHandler(IObjectIdProvider provider, ICompanyRepository repository)
    {
        this.provider = provider;
        this.repository = repository;
    }

    protected override async Task<CompanyPresentation[]> Handle(UserCompaniesQuery query)
    {
        var companyIds = await this.provider.List(query.UserId, "companies", "member");

        if (!companyIds.Any())
        {
            return Array.Empty<CompanyPresentation>();
        }
        
        return await this.repository.List(companyIds.Select(id => Guid.Parse(id)).ToArray());
    }
}