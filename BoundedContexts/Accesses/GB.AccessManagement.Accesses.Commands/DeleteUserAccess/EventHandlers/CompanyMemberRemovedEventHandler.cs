using GB.AccessManagement.Companies.Contracts.Events.Companies;
using GB.AccessManagement.Core.Events;
using MediatR;

namespace GB.AccessManagement.Accesses.Commands.DeleteUserAccess.EventHandlers;

public sealed class CompanyMemberRemovedEventHandler : DomainEventHandler<CompanyMemberRemovedEvent>
{
    private const string ObjectType = "companies";
    private const string Relation = "member";
    private readonly IMediator mediator;

    public CompanyMemberRemovedEventHandler(IMediator mediator)
    {
        this.mediator = mediator;
    }

    protected override async Task Handle(CompanyMemberRemovedEvent @event)
    {
        var command = new DeleteUserAccessCommand(@event.MemberId, ObjectType, @event.CompanyId, Relation);
        await this.mediator.Send(command);
    }
}