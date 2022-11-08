using GB.AccessManagement.Companies.Contracts.ValueTypes;
using GB.AccessManagement.Core.Queries;
using GB.AccessManagement.Core.ValueTypes;

namespace GB.AccessManagement.Companies.Queries.GetCompanyOwner;

public sealed record CompanyOwnerQuery(CompanyId CompanyId) : IQuery<UserId>;