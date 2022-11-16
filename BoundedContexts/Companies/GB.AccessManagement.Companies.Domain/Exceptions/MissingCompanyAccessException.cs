using GB.AccessManagement.Core.Exceptions;

namespace GB.AccessManagement.Companies.Domain.Exceptions;

[Serializable]
public sealed class MissingCompanyAccessException : DomainException
{
    private const string MessagePattern = "User '{0}' is not {1} of company '{2}'.";
    
    public MissingCompanyAccessException(Guid userId, string relation, Guid companyId)
        : base(string.Format(MessagePattern, userId, relation, companyId)) { }
}