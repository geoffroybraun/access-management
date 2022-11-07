using GB.AccessManagement.Companies.Contracts.Events.Companies;
using GB.AccessManagement.Companies.Contracts.ValueTypes;
using GB.AccessManagement.Core.Aggregates;

namespace GB.AccessManagement.Companies.Domain.Aggregates;

public sealed class CompanyAggregate : AggregateRoot
{
    private readonly CompanyName name;
    private readonly UserId ownerId;
    
    public CompanyId Id { get; }

    private CompanyAggregate(CompanyId id, CompanyName name, UserId ownerId)
    {
        this.Id = id;
        this.name = name;
        this.ownerId = ownerId;
    }

    public static CompanyAggregate Create(CompanyName name, UserId ownerId)
    {
        var aggregate = new CompanyAggregate(Guid.NewGuid(), name, ownerId);
        aggregate.StoreEvent(new CompanyCreatedEvent(aggregate.Id, aggregate.name, aggregate.ownerId));
        
        return aggregate;
    }
}