using GB.AccessManagement.Accesses.Contracts.Commands;
using GB.AccessManagement.Companies.Domain.Events.Companies;
using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Core.Events;
using MediatR;

namespace GB.AccessManagement.Companies.Commands.CreateCompany.DomainEventHandlers;

public sealed class CompanyCreatedEventHandler : DomainEventHandler<CompanyCreatedEvent>
{
    private const string ObjectType = "companies";
    private const string OwnerRelation = "owner";
    private const string ParentRelation = "parent";
    private readonly IMediator mediator;

    public CompanyCreatedEventHandler(IMediator mediator)
    {
        this.mediator = mediator;
    }

    protected override async Task Handle(CompanyCreatedEvent @event)
    {
        await this.CreateOwnerAccess(@event.Id, @event.OwnerId);

        if (@event.ParentCompanyId is not null)
        {
            await this.CreateParentAccess(@event.Id, @event.ParentCompanyId);
        }
    }

    private async Task CreateOwnerAccess(CompanyId companyId, UserId ownerId)
    {
        CreateUserAccessCommand command = new(ownerId, ObjectType, companyId, OwnerRelation);
        _ = await this.mediator.Send(command);
    }

    private async Task CreateParentAccess(CompanyId companyId, CompanyId parentCompanyId)
    {
        CreateObjectAccessCommand command = new(
            ObjectType,
            parentCompanyId,
            ObjectType,
            companyId,
            ParentRelation);
        _ = await this.mediator.Send(command);
    }
}