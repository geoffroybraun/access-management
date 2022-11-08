using GB.AccessManagement.Accesses.Contracts.ValueTypes;
using GB.AccessManagement.Companies.Domain.Aggregates;
using GB.AccessManagement.Companies.Infrastructure.Contexts;
using GB.AccessManagement.Companies.Infrastructure.Daos;
using GB.AccessManagement.Companies.Queries;
using GB.AccessManagement.Core.Aggregates.Memos;
using GB.AccessManagement.Core.Events.Publishers;
using GB.AccessManagement.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace GB.AccessManagement.Companies.Infrastructure;

public sealed class CompanyRepository : Commands.ICompanyRepository, Queries.ICompanyRepository, IScopedService
{
    private readonly CompanyDbContext dbContext;
    private readonly IDomainEventPublisher publisher;

    public CompanyRepository(CompanyDbContext dbContext, IDomainEventPublisher publisher)
    {
        this.publisher = publisher;
        this.dbContext = dbContext;
    }

    async Task Commands.ICompanyRepository.Save(CompanyAggregate aggregate)
    {
        var dao = await FindAsync(aggregate.Id);

        foreach (var @event in aggregate.UncommittedEvents)
        {
            aggregate.Save(dao, @event);
        }

        await this.Save(dao);
        await this.publisher.Publish(aggregate.UncommittedEvents);
    }

    async Task<CompanyPresentation[]> ICompanyRepository.List(ObjectId[] ids)
    {
        Guid[] companyIds = ids
            .Select(id => Guid.Parse(id.ToString()))
            .ToArray();
        
        return await dbContext
            .Companies
            .AsNoTracking()
            .Where(company => companyIds.Contains(company.Id))
            .Select(company => new CompanyPresentation(company.Id, company.Name))
            .ToArrayAsync();
    }

    private async Task<CompanyDao> FindAsync(Guid companyId)
    {
        var dao = await this.dbContext.Companies.SingleOrDefaultAsync(company => company.Id == companyId);

        return dao ?? new CompanyDao();
    }

    private async Task Save(IAggregateMemo memo)
    {
        switch (memo.State)
        {
            case EMemoState.Created:
                await this.dbContext.AddAsync(memo);
                break;
            
            case EMemoState.Updated:
                this.dbContext.Update(memo);
                break;
            
            case EMemoState.Deleted:
                this.dbContext.Remove(memo);
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(memo.State));
        }

        await this.dbContext.SaveChangesAsync();
    }
}