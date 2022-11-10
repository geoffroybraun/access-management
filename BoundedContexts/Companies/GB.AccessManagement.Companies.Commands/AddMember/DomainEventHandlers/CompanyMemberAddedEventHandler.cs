using GB.AccessManagement.Accesses.Contracts.Commands;
using GB.AccessManagement.Companies.Domain.Events.Companies;
using GB.AccessManagement.Core.Events;
using MediatR;

namespace GB.AccessManagement.Companies.Commands.AddMember.DomainEventHandlers;

public sealed class CompanyMemberAddedEventHandler : DomainEventHandler<CompanyMemberAddedEvent>
{
    private const string ObjectType = "companies";
    private const string Relation = "member";
    private readonly IMediator mediator;

    public CompanyMemberAddedEventHandler(IMediator mediator)
    {
        this.mediator = mediator;
    }

    protected override async Task Handle(CompanyMemberAddedEvent @event)
    {
        var command = new CreateUserAccessCommand(@event.MemberId, ObjectType, @event.CompanyId, Relation);
        await this.mediator.Send(command);
    }
}