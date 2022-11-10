using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Companies.Contracts.Queries;

public sealed record CompanyMembersQuery(Guid CompanyId) : IQuery<Guid[]>;