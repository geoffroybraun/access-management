using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Companies.Contracts.Commands;

public sealed record RemoveMemberCommand(Guid CompanyId, Guid MemberId) : ICommand;