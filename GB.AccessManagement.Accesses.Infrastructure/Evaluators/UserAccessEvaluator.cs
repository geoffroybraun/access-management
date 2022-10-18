using GB.AccessManagement.Accesses.Domain.Evaluators;
using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Core.Services;

namespace GB.AccessManagement.Accesses.Infrastructure.Evaluators;

public sealed class UserAccessEvaluator : IUserAccessEvaluator, ISingletonService
{
    public Task<bool> CanAccess(UserAccess access)
    {
        return Task.FromResult(true);
    }
}