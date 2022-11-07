using GB.AccessManagement.Core.Services;
using MediatR;

namespace GB.AccessManagement.Core.Events.Publishers;

public sealed class DomainEventPublisher : IDomainEventPublisher, IScopedService
{
    private readonly IMediator mediator;

    public DomainEventPublisher(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task Publish(DomainEvent[] events)
    {
        for (int i = 0; i < events.Length; i++)
        {
            await this.Publish(events[i]);
        }
    }

    public async Task Publish<TEvent>(TEvent @event) where TEvent : DomainEvent
    {
        await this.mediator.Publish(@event);
        @event.Commit();
    }
}