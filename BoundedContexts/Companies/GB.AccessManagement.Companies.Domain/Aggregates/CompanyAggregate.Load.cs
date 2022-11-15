using GB.AccessManagement.Companies.Domain.Factories;
using GB.AccessManagement.Companies.Domain.Memos;
using GB.AccessManagement.Core.Services;

namespace GB.AccessManagement.Companies.Domain.Aggregates;

public sealed partial class CompanyAggregate
{
    public sealed class CompanyAggregateLoader : ICompanyAggregateLoader, IScopedService
    {
        public CompanyAggregate Load(ICompanyMemo memo)
        {
            var aggregate = new CompanyAggregate(memo.Id, memo.Name, memo.OwnerId, memo.ParentCompanyId);
                
            if (memo.Members.Any())
            {
                aggregate.members.AddRange(memo.Members);
            }

            return aggregate;
        }
    }
}