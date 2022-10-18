using Microsoft.AspNetCore.Authorization;

namespace GB.AccessManagement.WebApi.Authorization;

public sealed record UserAccessAuthorizationRequirement(string ObjectType, string ObjectId, string Relation) : IAuthorizationRequirement { }