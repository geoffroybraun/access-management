using GB.AccessManagement.Core.Aggregates.Memos;
using GB.AccessManagement.Core.Events;

namespace GB.AccessManagement.Core.Aggregates.Events;

public interface IEventApplierAggregate<in TEvent, in TMemo>
    where TEvent : DomainEvent
    where TMemo : IAggregateMemo
{
    void Apply(TEvent @event, TMemo memo);
}