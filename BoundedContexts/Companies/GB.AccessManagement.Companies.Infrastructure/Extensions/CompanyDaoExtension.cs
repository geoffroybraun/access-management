using GB.AccessManagement.Companies.Contracts.Presentations;
using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Companies.Infrastructure.Daos;

namespace GB.AccessManagement.Companies.Infrastructure.Extensions;

internal static class CompanyDaoExtension
{
    public static IQueryable<CompanyDao> Filter(this IQueryable<CompanyDao> companies, IEnumerable<CompanyId> ids)
    {
        Guid[] companyIds = ids
            .Select(id => Guid.Parse(id.ToString()))
            .ToArray();

        return companies.Where(company => companyIds.Contains(company.Id));
    }

    public static IQueryable<CompanyDao> ForCompany(this IQueryable<CompanyDao> companies, Guid companyId)
    {
        return companies.Where(company => company.Id == companyId);
    }

    public static IQueryable<CompanyPresentation> AsPresentations(this IQueryable<CompanyDao> companies)
    {
        return companies.Select(company => new CompanyPresentation(company.Id, company.Name));
    }
}