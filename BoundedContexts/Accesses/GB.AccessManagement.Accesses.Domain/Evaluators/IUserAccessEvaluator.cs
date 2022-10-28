using GB.AccessManagement.Accesses.Domain.ValueTypes;

namespace GB.AccessManagement.Accesses.Domain.Evaluators;

public interface IUserAccessEvaluator
{
    Task<bool> CanAccess(UserAccess access);
}