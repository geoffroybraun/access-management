namespace GB.AccessManagement.WebApi.Authorization;

public static class UserAccessAuthorizationPolicyBuilder
{
    public const string PolicyPrefix = "Object";
    
    public static string Build(string objectType, string objectId, string relation)
    {
        return $"{PolicyPrefix}:{objectType}:{objectId}:{relation}";
    }
}