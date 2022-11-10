using GB.AccessManagement.Accesses.Contracts.Presentations;
using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Contracts.Queries;

public sealed record ListUserAccessesQuery(string UserId, string ObjectType) : IQuery<UserAccessPresentation[]>;