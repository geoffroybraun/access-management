using GB.AccessManagement.Accesses.Contracts.Presentations;
using GB.AccessManagement.Core.Queries;

namespace GB.AccessManagement.Accesses.Contracts.Queries;

public sealed record GetUserAccessQuery(Guid UserId, string ObjectType, string ObjectId) : IQuery<UserAccessPresentation?>;