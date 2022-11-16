using GB.AccessManagement.Companies.Domain.ValueTypes;

namespace GB.AccessManagement.Companies.Domain.Policies;

public interface ICompanyOwnerPolicy
{
    Task EnsureUserIsCompanyOwner(UserId userId, CompanyId companyId);
}