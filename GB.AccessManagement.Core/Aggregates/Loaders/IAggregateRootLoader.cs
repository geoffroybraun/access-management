using GB.AccessManagement.Core.Aggregates.Memos;

namespace GB.AccessManagement.Core.Aggregates.Loaders;

public interface IAggregateRootLoader<out TAggregate, TAggregateId, in TAggregateMemo>
    where TAggregate : AggregateRoot<TAggregate, TAggregateId, TAggregateMemo>
    where TAggregateId : notnull
    where TAggregateMemo : IAggregateMemo
{
    TAggregate Load(TAggregateMemo memo);
}