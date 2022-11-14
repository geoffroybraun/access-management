using GB.AccessManagement.Companies.Domain.Events.Companies;
using GB.AccessManagement.Companies.Domain.Memos;
using GB.AccessManagement.Core.Aggregates.Memos;

namespace GB.AccessManagement.Companies.Domain.Aggregates;

public sealed partial class CompanyAggregate
{
    public void Save(ICompanyMemo memo, CompanyCreatedEvent @event)
    {
        memo.State = EMemoState.Created;
        memo.Id = @event.Id;
        memo.Name = @event.Name;
    }

    public void Save(ICompanyMemo memo, CompanyMemberAddedEvent @event)
    {
        memo.State = EMemoState.Unchanged;
    }

    public void Save(ICompanyMemo memo, CompanyMemberRemovedEvent @event)
    {
        memo.State = EMemoState.Unchanged;
    }

    public void Save(ICompanyMemo memo, CompanyAttachedToParentEvent @event)
    {
        memo.ParentCompanyId = this.parentCompanyId;
    }
}