using GB.AccessManagement.Core.Events;

namespace GB.AccessManagement.Core.Aggregates;

public abstract class AggregateRoot : IEventDrivenAggregate
{
    private readonly HashSet<DomainEvent> storedEvents = new();

    public DomainEvent[] UncommittedEvents => this.storedEvents.Where(@event => !@event.HasBeenCommitted).ToArray();
    
    public void StoreEvent(DomainEvent domainEvent)
    {
        this.storedEvents.Add(domainEvent);
    }
}