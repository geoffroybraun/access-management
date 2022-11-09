using GB.AccessManagement.Companies.Contracts.ValueTypes;
using GB.AccessManagement.Companies.Domain.Aggregates;

namespace GB.AccessManagement.Companies.Commands;

public interface ICompanyRepository
{
    Task Save(CompanyAggregate aggregate);

    Task<CompanyAggregate> Load(CompanyId companyId);
}