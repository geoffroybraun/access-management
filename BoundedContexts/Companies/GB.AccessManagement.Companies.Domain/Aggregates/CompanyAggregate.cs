using GB.AccessManagement.Companies.Contracts.Events.Companies;
using GB.AccessManagement.Companies.Contracts.ValueTypes;
using GB.AccessManagement.Companies.Domain.Memos;
using GB.AccessManagement.Core.Aggregates;

namespace GB.AccessManagement.Companies.Domain.Aggregates;

public sealed class CompanyAggregate : AggregateRoot<CompanyAggregate, ICompanyMemo>
{
    private readonly CompanyName name;
    private UserId ownerId;
    
    public CompanyId Id { get; }

    private CompanyAggregate(CompanyId id, CompanyName name)
    {
        this.Id = id;
        this.name = name;
    }

    public static CompanyAggregate Create(CompanyName name)
    {
        var aggregate = new CompanyAggregate(Guid.NewGuid(), name);
        aggregate.StoreEvent(new CompanyCreatedEvent(aggregate.Id, aggregate.name));
        
        return aggregate;
    }

    public void DefineOwnerId(UserId ownerId)
    {
        this.ownerId = ownerId;
        this.StoreEvent(new CompanyOwnerDefinedEvent(this.Id, this.ownerId));
    }

    public void Save(ICompanyMemo memo, CompanyCreatedEvent @event)
    {
        memo.Id = @event.Id;
        memo.Name = @event.Name;
    }
}