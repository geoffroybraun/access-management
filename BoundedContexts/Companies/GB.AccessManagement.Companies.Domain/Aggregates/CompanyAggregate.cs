using GB.AccessManagement.Companies.Domain.Events.Companies;
using GB.AccessManagement.Companies.Domain.Memos;
using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Core.Aggregates;

namespace GB.AccessManagement.Companies.Domain.Aggregates;

public sealed partial class CompanyAggregate : AggregateRoot<CompanyAggregate, ICompanyMemo>
{
    private readonly List<UserId> members = new();
    private CompanyName name;
    private UserId? ownerId;
    private CompanyId? parentCompanyId;

    public CompanyId Id { get; private set; }

    public CompanyAggregate() { }
    
    private CompanyAggregate(CompanyId id, CompanyName name)
    {
        this.Id = id;
        this.name = name;
    }

    public void AddMember(UserId memberId)
    {
        this.members.Add(memberId);
        this.StoreEvent(new CompanyMemberAddedEvent(this.Id, memberId));
    }

    public void RemoveMember(UserId memberId)
    {
        this.members.Remove(memberId);
        this.StoreEvent(new CompanyMemberRemovedEvent(this.Id, memberId));
    }

    public override CompanyAggregate Load(ICompanyMemo memo)
    {
        this.Id = memo.Id;
        this.name = memo.Name;

        if (memo.Members.Any())
        {
            this.members.AddRange(memo.Members.Select(user => user));
        }

        if (memo.ParentCompanyId is not null)
        {
            this.parentCompanyId = memo.ParentCompanyId;
        }

        return this;
    }
}