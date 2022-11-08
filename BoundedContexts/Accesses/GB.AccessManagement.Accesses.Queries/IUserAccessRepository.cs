using GB.AccessManagement.Accesses.Contracts.ValueTypes;
using GB.AccessManagement.Core.ValueTypes;

namespace GB.AccessManagement.Accesses.Queries;

public interface IUserAccessRepository
{
    Task<string?> GetRelation(UserId userId, ObjectType objectType, ObjectId objectId);

    Task<UserAccessPresentation[]> List(UserId userId, ObjectType objectType);
}