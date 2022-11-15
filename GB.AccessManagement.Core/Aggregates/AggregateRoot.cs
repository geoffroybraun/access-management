using GB.AccessManagement.Core.Aggregates.Events;
using GB.AccessManagement.Core.Aggregates.Memos;
using GB.AccessManagement.Core.Events;

namespace GB.AccessManagement.Core.Aggregates;

public abstract class AggregateRoot<TAggregate, TAggregateId, TMemo> : IEventDrivenAggregate, IMemorizableAggregate<TAggregate, TMemo>
    where TAggregate : AggregateRoot<TAggregate, TAggregateId, TMemo>
    where TAggregateId : notnull
    where TMemo : IAggregateMemo
{
    private readonly HashSet<DomainEvent> storedEvents = new();

    public TAggregateId Id { get; protected set; } = default!;

    public DomainEvent[] UncommittedEvents => this.storedEvents.Where(@event => !@event.HasBeenCommitted).ToArray();
    
    public void StoreEvent(DomainEvent domainEvent)
    {
        this.storedEvents.Add(domainEvent);
    }

    public void Save<TEvent>(TEvent @event, TMemo memo) where TEvent : DomainEvent
    {
        if (this is IEventApplierAggregate<TEvent, TMemo> eventApplier)
        {
            eventApplier.Apply(@event, memo);

            return;
        }
        
        throw new MissingMethodException();
    }
}