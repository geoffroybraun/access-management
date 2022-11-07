using GB.AccessManagement.Companies.Commands;
using GB.AccessManagement.Companies.Domain.Aggregates;
using GB.AccessManagement.Core.Services;
using MediatR;

namespace GB.AccessManagement.Companies.Infrastructure;

public sealed class CompanyRepository : ICompanyRepository, IScopedService
{
    private readonly IMediator mediator;

    public CompanyRepository(IMediator mediator)
    {
        this.mediator = mediator;
    }

    public async Task Save(CompanyAggregate aggregate)
    {
        foreach (var @event in aggregate.UncommittedEvents)
        {
            await this.mediator.Publish(@event);
            @event.Commit();;
        }
    }
}