using GB.AccessManagement.Core.Commands;

namespace GB.AccessManagement.Companies.Commands.CreateCompany;

public sealed record CreateCompanyCommand(string Name, Guid OwnerId) : ICommand<Guid>;