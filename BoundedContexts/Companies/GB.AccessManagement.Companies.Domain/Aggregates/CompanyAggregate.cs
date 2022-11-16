using GB.AccessManagement.Companies.Domain.Events.Companies;
using GB.AccessManagement.Companies.Domain.Exceptions;
using GB.AccessManagement.Companies.Domain.Memos;
using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Core.Aggregates;

namespace GB.AccessManagement.Companies.Domain.Aggregates;

public sealed partial class CompanyAggregate : AggregateRoot<CompanyAggregate, CompanyId, ICompanyMemo>
{
    private readonly List<UserId> members = new();
    private readonly CompanyName name;
    private readonly UserId ownerId;
    private readonly CompanyId? parentCompanyId;
    
    private CompanyAggregate(CompanyId id, CompanyName name, UserId ownerId, CompanyId? parentCompanyId)
    {
        this.Id = id;
        this.name = name;
        this.ownerId = ownerId;
        this.parentCompanyId = parentCompanyId;
    }

    public void AddMember(UserId memberId)
    {
        if (members.Contains(memberId))
        {
            throw new MemberAlreadyAddedException(memberId, this.Id);
        }
        
        this.members.Add(memberId);
        this.StoreEvent(new CompanyMemberAddedEvent(this.Id, memberId));
    }

    public void RemoveMember(UserId memberId)
    {
        if (!members.Contains(memberId))
        {
            throw new MissingCompanyMemberException(memberId, this.Id);
        }
        
        this.members.Remove(memberId);
        this.StoreEvent(new CompanyMemberRemovedEvent(this.Id, memberId));
    }
}