namespace GB.AccessManagement.Companies.Domain.Memos;

public interface ICompanyMemo
{
    Guid Id { get; set; }
    
    string Name { get; set; }
}