using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Queries.ListUserAccesses;

public sealed record ListUserAccessesQuery(string UserId, string ObjectType) : IQuery<UserAccessPresentation[]>;