using GB.AccessManagement.Accesses.Contracts.Commands;
using GB.AccessManagement.Companies.Domain.Events.Companies;
using GB.AccessManagement.Core.Events;
using MediatR;

namespace GB.AccessManagement.Companies.Commands.CreateCompany.DomainEventHandlers;

public sealed class CompanyAttachedToParentEventHandler : DomainEventHandler<CompanyAttachedToParentEvent>
{
    private const string ObjectType = "companies";
    private const string Relation = "parent";
    private readonly IMediator mediator;

    public CompanyAttachedToParentEventHandler(IMediator mediator)
    {
        this.mediator = mediator;
    }

    protected override async Task Handle(CompanyAttachedToParentEvent @event)
    {
        CreateObjectAccessCommand command = new(
            ObjectType,
            @event.ParentCompanyId,
            ObjectType,
            @event.CompanyId,
            Relation);
        _ = await this.mediator.Send(command);
    }
}