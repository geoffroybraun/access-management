using System.Security.Claims;
using GB.AccessManagement.Accesses.Domain.Evaluators;
using GB.AccessManagement.Accesses.Domain.ValueTypes;
using GB.AccessManagement.Core.Services;
using Microsoft.AspNetCore.Authorization;

namespace GB.AccessManagement.WebApi.Authorization;

public sealed class UserAccessAuthorizationHandler : AuthorizationHandler<UserAccessAuthorizationRequirement>, ISingletonService
{
    private readonly IUserAccessEvaluator evaluator;

    public UserAccessAuthorizationHandler(IUserAccessEvaluator evaluator)
    {
        this.evaluator = evaluator;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAccessAuthorizationRequirement requirement)
    {
        if (context.User.Identity?.IsAuthenticated == false)
        {
            return;
        }

        string userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        UserAccess access = new(userId, requirement.ObjectType, requirement.ObjectId, requirement.Relation);

        if (await this.evaluator.CanAccess(access))
        {
            context.Succeed(requirement);
        }
    }
}