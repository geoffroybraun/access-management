using GB.AccessManagement.Core.Exceptions;

namespace GB.AccessManagement.Companies.Domain.Exceptions;

[Serializable]
public sealed class MissingCompanyAccessException : DomainException
{
    private const string MessagePattern = "User '{0}' is not {1} of company '{2}'.";
    
    public MissingCompanyAccessException(string userId, string relation, string companyId)
        : base(string.Format(MessagePattern, userId, relation, companyId)) { }
}