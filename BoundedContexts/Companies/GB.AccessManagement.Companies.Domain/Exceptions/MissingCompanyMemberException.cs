using GB.AccessManagement.Core.Exceptions;

namespace GB.AccessManagement.Companies.Domain.Exceptions;

[Serializable]
public sealed class MissingCompanyMemberException : DomainException
{
    private const string MessagePattern = "User '{0}' is not a member of company '{1}'.";
    
    public MissingCompanyMemberException(Guid memberId, Guid companyId)
        : base(string.Format(MessagePattern, memberId, companyId)) { }
}