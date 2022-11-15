using GB.AccessManagement.Accesses.Contracts.Queries;
using GB.AccessManagement.Companies.Commands;
using GB.AccessManagement.Companies.Contracts.Presentations;
using GB.AccessManagement.Companies.Domain.Aggregates;
using GB.AccessManagement.Companies.Domain.Factories;
using GB.AccessManagement.Companies.Domain.Memos;
using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Companies.Infrastructure.Contexts;
using GB.AccessManagement.Companies.Infrastructure.Daos;
using GB.AccessManagement.Companies.Queries;
using GB.AccessManagement.Core.Aggregates.Memos;
using GB.AccessManagement.Core.Events.Publishers;
using GB.AccessManagement.Core.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GB.AccessManagement.Companies.Infrastructure;

public sealed class CompanyRepository : ICompanyStore, ICompanyRepository, IScopedService
{
    private const string ObjectType = "companies";
    private const string Relation = "member";
    private readonly CompanyDbContext dbContext;
    private readonly IDomainEventPublisher publisher;
    private readonly IMediator mediator;
    private readonly ICompanyAggregateLoader loader;

    public CompanyRepository(
        CompanyDbContext dbContext,
        IDomainEventPublisher publisher,
        IMediator mediator,
        ICompanyAggregateLoader loader)
    {
        this.dbContext = dbContext;
        this.mediator = mediator;
        this.loader = loader;
        this.publisher = publisher;
    }

    public async Task<CompanyAggregate> Load(CompanyId id)
    {
        ICompanyMemo memo = await FindAsync(id);
        var members = await this.mediator.Send(new ListObjectUserIdsQuery(ObjectType, id.ToString(), Relation));
        memo.Members = new List<UserId>(members.Select(member => (UserId)member));

        return loader.Load(memo);
    }

    public async Task Save(CompanyAggregate aggregate)
    {
        var dao = await FindAsync(aggregate.Id);

        foreach (var @event in aggregate.UncommittedEvents)
        {
            aggregate.Save(dao, @event);
        }

        await this.Save(dao);
        await this.publisher.Publish(aggregate.UncommittedEvents);
    }

    public async Task<CompanyPresentation[]> List(CompanyId[] ids)
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

    public async Task<CompanyPresentation> Get(CompanyId id)
    {
        return await this.dbContext
            .Companies
            .AsNoTracking()
            .Where(company => company.Id == (Guid) id)
            .Select(company => new CompanyPresentation(company.Id, company.Name))
            .SingleAsync();
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
            
            case EMemoState.Unchanged:
                break;
            
            default:
                throw new ArgumentOutOfRangeException(nameof(memo.State));
        }

        await this.dbContext.SaveChangesAsync();
    }
}