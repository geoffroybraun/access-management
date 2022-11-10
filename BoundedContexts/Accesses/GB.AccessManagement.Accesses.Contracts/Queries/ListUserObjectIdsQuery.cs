using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Contracts.Queries;

public sealed record ListUserObjectIdsQuery(string UserId, string ObjectType, string Relation) : IQuery<string[]>;