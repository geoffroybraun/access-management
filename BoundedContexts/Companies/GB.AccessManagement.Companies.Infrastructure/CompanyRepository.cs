using GB.AccessManagement.Companies.Commands;
using GB.AccessManagement.Companies.Domain.Aggregates;
using GB.AccessManagement.Companies.Infrastructure.Contexts;
using GB.AccessManagement.Companies.Infrastructure.Daos;
using GB.AccessManagement.Core.Events.Publishers;
using GB.AccessManagement.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace GB.AccessManagement.Companies.Infrastructure;

public sealed class CompanyRepository : ICompanyRepository, IScopedService
{
    private readonly CompanyDbContext dbContext;
    private readonly IDomainEventPublisher publisher;

    public CompanyRepository(CompanyDbContext dbContext, IDomainEventPublisher publisher)
    {
        this.publisher = publisher;
        this.dbContext = dbContext;
    }

    public async Task Save(CompanyAggregate aggregate)
    {
        var dao = await FindAsync(aggregate.Id);

        foreach (var @event in aggregate.UncommittedEvents)
        {
            aggregate.Save(dao, @event);
        }

        await this.dbContext.SaveChangesAsync();
        await this.publisher.Publish(aggregate.UncommittedEvents);
    }

    private async Task<CompanyDao> FindAsync(Guid companyId)
    {
        var dao = await this.dbContext.Companies.SingleOrDefaultAsync(company => company.Id == companyId);

        return dao ?? new CompanyDao();
    }
}