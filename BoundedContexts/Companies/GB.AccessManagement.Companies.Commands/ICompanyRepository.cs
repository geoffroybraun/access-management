using GB.AccessManagement.Companies.Domain.Aggregates;

namespace GB.AccessManagement.Companies.Commands;

public interface ICompanyRepository
{
    Task Save(CompanyAggregate aggregate);
}