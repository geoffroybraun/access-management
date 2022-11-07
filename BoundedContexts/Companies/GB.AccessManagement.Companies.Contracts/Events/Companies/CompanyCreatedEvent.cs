using GB.AccessManagement.Companies.Contracts.ValueTypes;
using GB.AccessManagement.Core.Events;

namespace GB.AccessManagement.Companies.Contracts.Events.Companies;

public sealed record CompanyCreatedEvent(CompanyId Id, CompanyName Name, UserId OwnerId) : DomainEvent;