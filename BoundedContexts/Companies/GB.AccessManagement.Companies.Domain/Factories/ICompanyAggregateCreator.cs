using GB.AccessManagement.Companies.Domain.Aggregates;
using GB.AccessManagement.Companies.Domain.ValueTypes;

namespace GB.AccessManagement.Companies.Domain.Factories;

public interface ICompanyAggregateCreator
{
    CompanyAggregate Create(CompanyName name, UserId ownerId, CompanyId? parentCompanyId);
}