using GB.AccessManagement.Accesses.Contracts.Queries;
using GB.AccessManagement.Companies.Contracts.Presentations;
using GB.AccessManagement.Companies.Contracts.Queries;
using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Core.Queries;
using MediatR;

namespace GB.AccessManagement.Companies.Queries.CompanyChildren;

public sealed class CompanChildrenQueryHandler : QueryHandler<CompanyChildrenQuery, CompanyPresentation[]>
{
    private const string ObjectType = "companies";
    private const string Relation = "parent";
    private readonly IMediator mediator;
    private readonly ICompanyRepository repository;

    public CompanChildrenQueryHandler(IMediator mediator, ICompanyRepository repository)
    {
        this.mediator = mediator;
        this.repository = repository;
    }

    protected override async Task<CompanyPresentation[]> Handle(CompanyChildrenQuery query)
    {
        var childrenCompanyIds = await this.GetCompanyChildrenIds(query.CompanyId);

        if (!childrenCompanyIds.Any())
        {
            return Array.Empty<CompanyPresentation>();
        }

        var companyIds = childrenCompanyIds
            .Select(id => (CompanyId)id)
            .ToArray();

        return await this.repository.List(companyIds);
    }

    private async Task<string[]> GetCompanyChildrenIds(Guid companyId)
    {
        var query = new UserObjectIdsQuery($"{ObjectType}:{companyId}", ObjectType, Relation);

        return await this.mediator.Send(query);
    }
}