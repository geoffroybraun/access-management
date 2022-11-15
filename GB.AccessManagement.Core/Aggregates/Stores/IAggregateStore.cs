using GB.AccessManagement.Core.Aggregates.Memos;

namespace GB.AccessManagement.Core.Aggregates.Stores;

public interface IAggregateStore<TAggregate, TAggregateId, in TMemo>
    where TAggregate : AggregateRoot<TAggregate, TAggregateId, TMemo>
    where TAggregateId : notnull
    where TMemo : IAggregateMemo
{
    Task Save(TAggregate aggregate);

    Task<TAggregate> Load(TAggregateId id);
}