using GB.AccessManagement.Companies.Contracts.ValueTypes;
using GB.AccessManagement.Core.Events;

namespace GB.AccessManagement.Companies.Contracts.Events.Companies;

public sealed record CompanyOwnerDefinedEvent(CompanyId CompanyId, UserId OwnerId) : DomainEvent;