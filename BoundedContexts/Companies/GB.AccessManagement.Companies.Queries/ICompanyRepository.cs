using GB.AccessManagement.Companies.Contracts.Presentations;
using GB.AccessManagement.Companies.Domain.ValueTypes;

namespace GB.AccessManagement.Companies.Queries;

public interface ICompanyRepository
{
    Task<bool> Exist(CompanyId id);
    
    Task<CompanyPresentation[]> List(CompanyId[] ids);

    Task<CompanyPresentation> Get(CompanyId id);
}