using GB.AccessManagement.Companies.Contracts.ValueTypes;
using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Companies.Queries.ListUserCompanies;

public sealed record UserCompaniesQuery(UserId UserId) : IQuery<CompanyPresentation[]>;