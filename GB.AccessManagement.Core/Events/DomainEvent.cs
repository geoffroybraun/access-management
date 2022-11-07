using MediatR;

namespace GB.AccessManagement.Core.Events;

public abstract record DomainEvent : INotification
{
    internal bool HasBeenCommitted { get; private set; }

    public void Commit()
    {
        this.HasBeenCommitted = true;
    }
}