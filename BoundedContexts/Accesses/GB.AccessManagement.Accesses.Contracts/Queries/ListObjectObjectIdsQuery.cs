using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Contracts.Queries;

public sealed record ListObjectObjectIdsQuery(string ObjectType, string ObjectId, string Relation) : IQuery<string[]>;