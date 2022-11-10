using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Core.Aggregates.Memos;

namespace GB.AccessManagement.Companies.Domain.Memos;

public interface ICompanyMemo : IAggregateMemo
{
    CompanyId Id { get; set; }
    
    CompanyName Name { get; set; }
    
    ICollection<UserId> Members { get; set; }
}