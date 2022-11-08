using GB.AccessManagement.Companies.Contracts.ValueTypes;
using GB.AccessManagement.Core.Queries;
using GB.AccessManagement.Core.ValueTypes;

namespace GB.AccessManagement.Companies.Queries.CompanyMembers;

public sealed record CompanyMembersQuery(CompanyId CompanyId) : IQuery<UserId[]>;