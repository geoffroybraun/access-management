using GB.AccessManagement.Companies.Domain.Events.Companies;
using GB.AccessManagement.Companies.Domain.Factories;
using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Core.Services;

namespace GB.AccessManagement.Companies.Domain.Aggregates;

public sealed partial class CompanyAggregate
{
    public sealed class CompanyAggregateCreator : ICompanyAggregateCreator, IScopedService
    {
        public CompanyAggregate Create(CompanyName name, UserId ownerId, CompanyId? parentCompanyId)
        {
            var aggregate = new CompanyAggregate(Guid.NewGuid(), name, ownerId, parentCompanyId);
            aggregate.StoreEvent(new CompanyCreatedEvent(aggregate.Id, aggregate.name, aggregate.ownerId, aggregate.parentCompanyId));

            return aggregate;
        }
    }
}