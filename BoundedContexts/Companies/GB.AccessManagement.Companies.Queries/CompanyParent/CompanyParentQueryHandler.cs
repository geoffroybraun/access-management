using GB.AccessManagement.Accesses.Contracts.Queries;
using GB.AccessManagement.Companies.Contracts.Presentations;
using GB.AccessManagement.Companies.Contracts.Queries;
using GB.AccessManagement.Core.Queries;
using MediatR;

namespace GB.AccessManagement.Companies.Queries.CompanyParent;

public sealed class CompanyParentQueryHandler : QueryHandler<CompanyParentQuery, CompanyPresentation?>
{
    private readonly IMediator mediator;
    private readonly ICompanyRepository repository;

    public CompanyParentQueryHandler(IMediator mediator, ICompanyRepository repository)
    {
        this.mediator = mediator;
        this.repository = repository;
    }

    protected override async Task<CompanyPresentation?> Handle(CompanyParentQuery query)
    {
        var parentCompanyIdsQuery = new ListObjectObjectIdsQuery("companies", query.CompanyId.ToString(), "parent");
        var parentCompanyIds = await this.mediator.Send(parentCompanyIdsQuery);

        if (!parentCompanyIds.Any())
        {
            return default;
        }

        var parentCompanyId = parentCompanyIds
            .Single()
            .Split(':')
            .Last();

        return await this.repository.Get(parentCompanyId);
    }
}