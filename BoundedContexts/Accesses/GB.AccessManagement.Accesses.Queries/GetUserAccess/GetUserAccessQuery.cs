using GB.AccessManagement.Accesses.Contracts.ValueTypes;
using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Queries.GetUserAccess;

public sealed record GetUserAccessQuery(string UserId, string ObjectType, string ObjectId) : IQuery<UserAccess?>;