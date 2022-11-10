using GB.AccessManagement.Accesses.Domain.ValueTypes;

namespace GB.AccessManagement.Accesses.Domain.Providers;

public interface IObjectIdProvider
{
    Task<ObjectId[]> List(UserId userId, ObjectType objectType, Relation relation);
}