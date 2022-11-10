using GB.AccessManagement.Accesses.Contracts.Commands;
using GB.AccessManagement.Companies.Domain.Events.Companies;
using GB.AccessManagement.Core.Events;
using MediatR;

namespace GB.AccessManagement.Companies.Commands.CreateCompany.DomainEventHandlers;

public sealed class CompanyOwnerDefinedEventHandler : DomainEventHandler<CompanyOwnerDefinedEvent>
{
    private const string ObjectType = "companies";
    private const string Relation = "owner";
    private readonly IMediator mediator;

    public CompanyOwnerDefinedEventHandler(IMediator mediator)
    {
        this.mediator = mediator;
    }

    protected override async Task Handle(CompanyOwnerDefinedEvent @event)
    {
        CreateUserAccessCommand command = new(@event.OwnerId, ObjectType, @event.CompanyId, Relation);
        await this.mediator.Send(command);
    }
}