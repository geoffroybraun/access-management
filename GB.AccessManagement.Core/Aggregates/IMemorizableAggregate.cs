using GB.AccessManagement.Core.Aggregates.Memos;
using GB.AccessManagement.Core.Events;

namespace GB.AccessManagement.Core.Aggregates;

public interface IMemorizableAggregate<out TAggregate, in TMemo>
    where TAggregate : IMemorizableAggregate<TAggregate, TMemo>
    where TMemo : IAggregateMemo
{
    void Save<TEvent>(TEvent @event, TMemo memo) where TEvent : DomainEvent;
}