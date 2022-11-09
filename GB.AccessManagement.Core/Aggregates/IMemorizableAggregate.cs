using GB.AccessManagement.Core.Aggregates.Memos;
using GB.AccessManagement.Core.Events;

namespace GB.AccessManagement.Core.Aggregates;

public interface IMemorizableAggregate<TAggregate, in TMemo>
    where TAggregate : IMemorizableAggregate<TAggregate, TMemo>, new()
    where TMemo : IAggregateMemo
{
    TAggregate Load(TMemo memo);
    
    void Save<TEvent>(TMemo memo, TEvent @event) where TEvent : DomainEvent;
}