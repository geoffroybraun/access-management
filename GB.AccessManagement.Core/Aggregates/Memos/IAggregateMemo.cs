namespace GB.AccessManagement.Core.Aggregates.Memos;

public interface IAggregateMemo
{
    EMemoState State { get; set; }
}