using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Companies.Contracts.Commands;

public sealed record AddMemberCommand(Guid CompanyId, Guid MemberId) : ICommand;