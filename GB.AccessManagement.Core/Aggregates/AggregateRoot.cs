using System.Reflection;
using GB.AccessManagement.Core.Aggregates.Memos;
using GB.AccessManagement.Core.Events;

namespace GB.AccessManagement.Core.Aggregates;

public abstract class AggregateRoot<TAggregate, TMemo> : IEventDrivenAggregate, IMemorizableAggregate<TMemo>
    where TAggregate : AggregateRoot<TAggregate, TMemo>
    where TMemo : IAggregateMemo
{
    private readonly HashSet<DomainEvent> storedEvents = new();

    public DomainEvent[] UncommittedEvents => this.storedEvents.Where(@event => !@event.HasBeenCommitted).ToArray();
    
    public void StoreEvent(DomainEvent domainEvent)
    {
        this.storedEvents.Add(domainEvent);
    }

    public void Save<TEvent>(TMemo memo, TEvent @event) where TEvent : DomainEvent
    {
        var methodInfo = typeof(TAggregate)
            .GetMethods()
            .SingleOrDefault(method => IsSaveMemoMethod(method, typeof(TMemo), @event.GetType()));

        try
        {
            methodInfo?.Invoke(this, new object[] { memo, @event });
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static bool IsSaveMemoMethod(MethodInfo method, Type memoType, Type eventType)
    {
        return method.Name == "Save"
               && method.GetParameters().Length == 2
               && method.GetParameters().First().ParameterType == memoType
               && method.GetParameters().Last().ParameterType == eventType;
    }
}