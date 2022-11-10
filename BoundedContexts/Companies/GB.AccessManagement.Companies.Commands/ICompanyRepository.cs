using GB.AccessManagement.Companies.Domain.Aggregates;
using GB.AccessManagement.Companies.Domain.ValueTypes;

namespace GB.AccessManagement.Companies.Commands;

public interface ICompanyRepository
{
    Task Save(CompanyAggregate aggregate);

    Task<CompanyAggregate> Load(CompanyId companyId);
}