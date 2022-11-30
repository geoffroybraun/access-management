using GB.AccessManagement.Accesses.Contracts.Queries;
using GB.AccessManagement.Companies.Contracts.Presentations;
using GB.AccessManagement.Companies.Contracts.Queries;
using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Core.Queries;
using MediatR;

namespace GB.AccessManagement.Companies.Queries.UserCompanies;

public sealed class UserCompaniesQueryHandler : QueryHandler<UserCompaniesQuery, CompanyPresentation[]>
{
    private const string OvjectType = "companies";
    private const string Relation = "member";
    private readonly ICompanyRepository repository;
    private readonly IMediator mediator;

    public UserCompaniesQueryHandler(ICompanyRepository repository, IMediator mediator)
    {
        this.repository = repository;
        this.mediator = mediator;
    }

    protected override async Task<CompanyPresentation[]> Handle(UserCompaniesQuery query)
    {
        var companyIdsQuery = new UserObjectIdsQuery(query.UserId.ToString(), OvjectType, Relation);
        var companyIds = await this.mediator.Send(companyIdsQuery);

        if (!companyIds.Any())
        {
            return Array.Empty<CompanyPresentation>();
        }
        
        return await this.repository.List(companyIds.Select(id => (CompanyId)Guid.Parse(id)).ToArray());
    }
}