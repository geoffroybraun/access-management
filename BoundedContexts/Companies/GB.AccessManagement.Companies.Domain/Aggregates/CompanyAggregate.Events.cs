using GB.AccessManagement.Companies.Domain.Events.Companies;
using GB.AccessManagement.Companies.Domain.Memos;
using GB.AccessManagement.Core.Aggregates.Events;
using GB.AccessManagement.Core.Aggregates.Memos;

namespace GB.AccessManagement.Companies.Domain.Aggregates;

public sealed partial class CompanyAggregate :
    IEventApplierAggregate<CompanyCreatedEvent, ICompanyMemo>,
    IEventApplierAggregate<CompanyOwnerDefinedEvent, ICompanyMemo>,
    IEventApplierAggregate<CompanyAttachedToParentEvent, ICompanyMemo>,
    IEventApplierAggregate<CompanyMemberAddedEvent, ICompanyMemo>,
    IEventApplierAggregate<CompanyMemberRemovedEvent, ICompanyMemo>
{
    public void Apply(CompanyCreatedEvent @event, ICompanyMemo memo)
    {
        memo.State = EMemoState.Created;
        memo.Id = @event.Id;
        memo.Name = @event.Name;
    }

    public void Apply(CompanyOwnerDefinedEvent @event, ICompanyMemo memo)
    {
        memo.OwnerId = @event.OwnerId;
    }

    public void Apply(CompanyAttachedToParentEvent @event, ICompanyMemo memo)
    {
        memo.ParentCompanyId = this.parentCompanyId;
    }

    public void Apply(CompanyMemberAddedEvent @event, ICompanyMemo memo)
    {
        memo.State = EMemoState.Unchanged;
    }

    public void Apply(CompanyMemberRemovedEvent @event, ICompanyMemo memo)
    {
        memo.State = EMemoState.Unchanged;
    }
}