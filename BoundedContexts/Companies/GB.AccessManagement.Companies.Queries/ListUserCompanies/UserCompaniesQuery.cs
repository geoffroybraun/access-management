using GB.AccessManagement.Core.Queries;
using GB.AccessManagement.Core.ValueTypes;

namespace GB.AccessManagement.Companies.Queries.ListUserCompanies;

public sealed record UserCompaniesQuery(UserId UserId) : IQuery<CompanyPresentation[]>;