using GB.AccessManagement.Companies.Contracts.Events.Companies;
using GB.AccessManagement.Companies.Contracts.ValueTypes;
using GB.AccessManagement.Companies.Domain.Memos;
using GB.AccessManagement.Core.Aggregates;
using GB.AccessManagement.Core.Aggregates.Memos;
using GB.AccessManagement.Core.ValueTypes;

namespace GB.AccessManagement.Companies.Domain.Aggregates;

public sealed class CompanyAggregate : AggregateRoot<CompanyAggregate, ICompanyMemo>
{
    private CompanyName name;
    private List<UserId> members;
    private UserId? ownerId;

    public CompanyId Id { get; private set; }

    public CompanyAggregate() { }
    
    private CompanyAggregate(CompanyId id, CompanyName name)
    {
        this.Id = id;
        this.name = name;
        this.members = new();
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

    public void AddMember(UserId memberId)
    {
        this.members.Add(memberId);
        this.StoreEvent(new CompanyMemberAddedEvent(this.Id, memberId));
    }

    public void Save(ICompanyMemo memo, CompanyCreatedEvent @event)
    {
        memo.State = EMemoState.Created;
        memo.Id = @event.Id;
        memo.Name = @event.Name;
    }

    public void Save(ICompanyMemo memo, CompanyMemberAddedEvent @event)
    {
        memo.State = EMemoState.Updated;
    }

    public override CompanyAggregate Load(ICompanyMemo memo)
    {
        this.Id = memo.Id;
        this.name = memo.Name;
        
        this.members = new();
        this.members.AddRange(memo.Members.Select(user => (UserId)user));

        return this;
    }
}