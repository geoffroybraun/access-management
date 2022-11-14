using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Companies.Contracts.Commands;

public sealed record CreateCompanyCommand(string Name, Guid OwnerId, Guid? ParentCompanyId = null) : ICommand<Guid>;