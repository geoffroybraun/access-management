using GB.AccessManagement.Core.Exceptions;

namespace GB.AccessManagement.Companies.Domain.Exceptions;

[Serializable]
public sealed class MemberAlreadyAddedException : DomainException
{
    private const string MessagePattern = "User '{0}' already is member of company '{1}'.";
    
    public MemberAlreadyAddedException(Guid memberId, Guid companyId)
        : base(string.Format(MessagePattern, memberId, companyId)) { }
}