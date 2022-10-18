using GB.AccessManagement.Core.Services;
using GB.AccessManagement.WebApi.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace GB.AccessManagement.WebApi.Authorization;

public sealed class UserAccessAuthorizationPolicyProvider : IAuthorizationPolicyProvider, ISingletonService
{
    public Task<AuthorizationPolicy?> GetPolicyAsync(string policyName)
    {
        if (!policyName.StartsWith(UserAccessAuthorizationPolicyBuilder.PolicyPrefix))
        {
            return this.GetFallbackPolicyAsync();
        }

        var policyBuilder = new AuthorizationPolicyBuilder();
        policyBuilder.AddRequirements(BuildRequirement(policyName));

        return Task.FromResult<AuthorizationPolicy?>(policyBuilder.Build());
    }

    public Task<AuthorizationPolicy> GetDefaultPolicyAsync()
    {
        var authorizationPolicy = new AuthorizationPolicyBuilder(DummyAuthenticationHandler.AuthenticationScheme)
            .RequireAuthenticatedUser()
            .Build();
        
        return Task.FromResult(authorizationPolicy);
    }

    public Task<AuthorizationPolicy?> GetFallbackPolicyAsync()
    {
        return Task.FromResult<AuthorizationPolicy?>(default);
    }

    private static UserAccessAuthorizationRequirement BuildRequirement(string policyName)
    {
        string[] policyValues = policyName.Split(':');
        string objectType = policyValues[0];
        string objectId = policyValues[1];
        string relation = policyValues[2];

        return new(objectType, objectId, relation);
    }
}