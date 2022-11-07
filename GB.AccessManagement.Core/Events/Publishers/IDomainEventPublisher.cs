namespace GB.AccessManagement.Core.Events.Publishers;

public interface IDomainEventPublisher
{
    Task Publish(DomainEvent[] events);

    Task Publish<TEvent>(TEvent @event) where TEvent : DomainEvent;
}