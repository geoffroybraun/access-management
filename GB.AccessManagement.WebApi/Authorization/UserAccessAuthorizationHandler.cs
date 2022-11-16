using System.Security.Claims;
using GB.AccessManagement.Accesses.Contracts.Queries;
using GB.AccessManagement.Core.Services;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace GB.AccessManagement.WebApi.Authorization;

public sealed class UserAccessAuthorizationHandler : AuthorizationHandler<UserAccessAuthorizationRequirement>, ISingletonService
{
    private readonly IMediator mediator;

    public UserAccessAuthorizationHandler(IMediator mediator)
    {
        this.mediator = mediator;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, UserAccessAuthorizationRequirement requirement)
    {
        if (context.User.Identity?.IsAuthenticated == false)
        {
            return;
        }

        string userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        var query = new CanAccessQuery(userId, requirement.ObjectType, requirement.ObjectId, requirement.Relation);

        if (await this.mediator.Send(query))
        {
            context.Succeed(requirement);
        }
    }
}