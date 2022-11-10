using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Companies.Contracts.Queries;

public sealed record CompanyOwnerQuery(Guid CompanyId) : IQuery<Guid>;