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
            var aggregate = new CompanyAggregate(memo.Id, memo.Name);
            aggregate.ownerId = memo.OwnerId;
                
            if (memo.Members.Any())
            {
                aggregate.members.AddRange(memo.Members);
            }

            if (memo.ParentCompanyId is not null)
            {
                aggregate.parentCompanyId = memo.ParentCompanyId;
            }

            return aggregate;
        }
    }
}