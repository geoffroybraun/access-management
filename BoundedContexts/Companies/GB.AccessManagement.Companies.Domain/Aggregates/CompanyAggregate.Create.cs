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
            var aggregate = CompanyAggregate.Create(Guid.NewGuid(), name);
            aggregate.DefineOwnerId(ownerId);

            if (parentCompanyId is not null)
            {
                aggregate.AttachToCompany(parentCompanyId);
            }

            return aggregate;
        }
    }

    private static CompanyAggregate Create(CompanyId id, CompanyName name)
    {
        var aggregate = new CompanyAggregate(Guid.NewGuid(), name);
        aggregate.StoreEvent(new CompanyCreatedEvent(aggregate.Id, aggregate.name));

        return aggregate;
    }

    private void DefineOwnerId(UserId ownerId)
    {
        this.ownerId = ownerId;
        this.StoreEvent(new CompanyOwnerDefinedEvent(this.Id, this.ownerId));
    }

    private void AttachToCompany(CompanyId parentCompanyId)
    {
        this.parentCompanyId = parentCompanyId;
        this.StoreEvent(new CompanyAttachedToParentEvent(this.Id, this.parentCompanyId));
    }
}