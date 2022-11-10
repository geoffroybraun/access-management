using GB.AccessManagement.Companies.Contracts.ValueTypes;
using GB.AccessManagement.Core.Commands;
using GB.AccessManagement.Core.ValueTypes;

namespace GB.AccessManagement.Companies.Commands.RemoveMember;

public sealed record RemoveMemberCommand(CompanyId CompanyId, UserId MemberId) : ICommand;