using GB.AccessManagement.Companies.Contracts.ValueTypes;
using GB.AccessManagement.Core.Aggregates.Memos;
using GB.AccessManagement.Core.ValueTypes;

namespace GB.AccessManagement.Companies.Domain.Memos;

public interface ICompanyMemo : IAggregateMemo
{
    CompanyId Id { get; set; }
    
    CompanyName Name { get; set; }
    
    ICollection<UserId> Members { get; set; }
}