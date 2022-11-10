using GB.AccessManagement.Companies.Contracts.Presentations;
using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Companies.Contracts.Queries;

public sealed record UserCompaniesQuery(Guid UserId) : IQuery<CompanyPresentation[]>;