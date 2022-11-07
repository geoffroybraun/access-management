using GB.AccessManagement.Core.Events;

namespace GB.AccessManagement.Core.Aggregates;

public interface IEventDrivenAggregate
{
    DomainEvent[] UncommittedEvents { get; }

    void StoreEvent(DomainEvent domainEvent);
}