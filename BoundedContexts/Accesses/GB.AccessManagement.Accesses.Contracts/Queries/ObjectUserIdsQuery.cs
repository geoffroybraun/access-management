using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Contracts.Queries;

public sealed record ObjectUserIdsQuery(string ObjectType, string ObjectId, string Relation, bool IsRecursive = false) : IQuery<string[]>;