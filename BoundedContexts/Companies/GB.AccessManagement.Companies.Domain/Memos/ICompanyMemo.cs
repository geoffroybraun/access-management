using GB.AccessManagement.Core.Aggregates.Memos;

namespace GB.AccessManagement.Companies.Domain.Memos;

public interface ICompanyMemo : IAggregateMemo
{
    Guid Id { get; set; }
    
    string Name { get; set; }
}