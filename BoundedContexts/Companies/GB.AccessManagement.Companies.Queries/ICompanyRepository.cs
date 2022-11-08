using GB.AccessManagement.Accesses.Contracts.ValueTypes;

namespace GB.AccessManagement.Companies.Queries;

public interface ICompanyRepository
{
    Task<CompanyPresentation[]> List(ObjectId[] ids);
}