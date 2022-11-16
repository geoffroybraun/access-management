using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Contracts.Queries;

public sealed record CanAccessQuery(string UserId, string ObjectType, string ObjectId, string Relation) : IQuery<bool>;