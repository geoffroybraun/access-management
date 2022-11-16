using GB.AccessManagement.Companies.Domain.Events.Companies;
using GB.AccessManagement.Companies.Domain.Factories;
using GB.AccessManagement.Companies.Domain.Policies;
using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Core.Services;

namespace GB.AccessManagement.Companies.Domain.Aggregates;

public sealed partial class CompanyAggregate
{
    public sealed class CompanyAggregateCreator : ICompanyAggregateCreator, IScopedService
    {
        private readonly ICompanyExistPolicy companyExistPolicy;

        public CompanyAggregateCreator(ICompanyExistPolicy companyExistPolicy)
        {
            this.companyExistPolicy = companyExistPolicy;
        }

        public async Task<CompanyAggregate> Create(CompanyName name, UserId ownerId, CompanyId? parentCompanyId)
        {
            if (parentCompanyId is not null)
            {
                await this.companyExistPolicy.EnsureCompanyExists(parentCompanyId);
            }
            
            var aggregate = new CompanyAggregate(Guid.NewGuid(), name, ownerId, parentCompanyId);
            aggregate.StoreEvent(new CompanyCreatedEvent(aggregate.Id, aggregate.name, aggregate.ownerId, aggregate.parentCompanyId));

            return aggregate;
        }
    }
}