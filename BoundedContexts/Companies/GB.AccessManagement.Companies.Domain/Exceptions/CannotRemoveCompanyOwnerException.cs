using GB.AccessManagement.Core.Exceptions;

namespace GB.AccessManagement.Companies.Domain.Exceptions;

[Serializable]
public sealed class CannotRemoveCompanyOwnerException : DomainException
{
    private const string MessagePattern = "User '{0}' is owner of company '{1}' and can not be removed.";
    
    public CannotRemoveCompanyOwnerException(Guid ownerId, Guid companyId)
        : base(string.Format(MessagePattern, ownerId, companyId)) { }
}