using MediatR;

namespace GB.AccessManagement.Core.Events;

public abstract class DomainEventHandler<TEvent> : INotificationHandler<TEvent> where TEvent : DomainEvent
{
    public async Task Handle(TEvent notification, CancellationToken cancellationToken)
    {
        await this.Handle(notification);
    }

    protected abstract Task Handle(TEvent @event);
}