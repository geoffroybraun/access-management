using GB.AccessManagement.Companies.Commands;
using GB.AccessManagement.Companies.Domain.Aggregates;
using GB.AccessManagement.Core.Events.Publishers;
using GB.AccessManagement.Core.Services;

namespace GB.AccessManagement.Companies.Infrastructure;

public sealed class CompanyRepository : ICompanyRepository, IScopedService
{
    private readonly IDomainEventPublisher publisher;

    public CompanyRepository(IDomainEventPublisher publisher)
    {
        this.publisher = publisher;
    }

    public async Task Save(CompanyAggregate aggregate)
    {
        await this.publisher.Publish(aggregate.UncommittedEvents);
    }
}