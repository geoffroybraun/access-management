using GB.AccessManagement.Companies.Domain.ValueTypes;
using GB.AccessManagement.Core.Exceptions;

namespace GB.AccessManagement.Companies.Domain.Exceptions;

[Serializable]
public sealed class NonExistentCompanyException : DomainException
{
    private const string MessagePattern = "Company {0} does not exist.";
    
    public NonExistentCompanyException(Guid id) : base(string.Format(MessagePattern, id)) { }
}