using GB.AccessManagement.Core.Aggregates.Memos;
using GB.AccessManagement.Core.Events;

namespace GB.AccessManagement.Core.Aggregates;

public interface IMemorizableAggregate<in TMemo> where TMemo : IAggregateMemo
{
    void Save<TEvent>(TMemo memo, TEvent @event) where TEvent : DomainEvent;
}