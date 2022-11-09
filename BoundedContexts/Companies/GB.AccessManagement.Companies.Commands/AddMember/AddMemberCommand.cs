using GB.AccessManagement.Companies.Contracts.ValueTypes;
using GB.AccessManagement.Core.Commands;
using GB.AccessManagement.Core.ValueTypes;

namespace GB.AccessManagement.Companies.Commands.AddMember;

public sealed record AddMemberCommand(CompanyId CompanyId, UserId MemberId) : ICommand;