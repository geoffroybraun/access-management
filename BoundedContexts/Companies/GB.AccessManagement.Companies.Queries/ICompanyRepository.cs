namespace GB.AccessManagement.Companies.Queries;

public interface ICompanyRepository
{
    Task<CompanyPresentation[]> List(Guid[] ids);
}