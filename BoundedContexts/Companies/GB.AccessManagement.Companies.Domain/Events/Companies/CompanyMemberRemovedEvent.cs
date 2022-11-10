using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Core.Events;

namespace GB.AccessManagement.Companies.Domain.Events.Companies;

public sealed record CompanyMemberRemovedEvent(CompanyId CompanyId, UserId MemberId) : DomainEvent;