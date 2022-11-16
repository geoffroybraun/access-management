using GB.AccessManagement.Companies.Domain.ValueTypes;

namespace GB.AccessManagement.Companies.Domain.Policies;

public interface ICompanyExistPolicy
{
    Task EnsureCompanyExists(CompanyId id);
}