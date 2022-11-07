using GB.AccessManagement.Accesses.Contracts.ValueTypes;

namespace GB.AccessManagement.Accesses.Contracts.Evaluators;

public interface IUserAccessEvaluator
{
    Task<bool> CanAccess(UserAccess access);
}