using GB.AccessManagement.Core.Events;

namespace GB.AccessManagement.Core.Aggregates.Events;

public interface IEventDrivenAggregate
{
    DomainEvent[] UncommittedEvents { get; }

    void StoreEvent(DomainEvent domainEvent);
}