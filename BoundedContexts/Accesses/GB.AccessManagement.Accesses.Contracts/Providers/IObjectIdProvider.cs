using GB.AccessManagement.Accesses.Contracts.ValueTypes;
using GB.AccessManagement.Core.ValueTypes;

namespace GB.AccessManagement.Accesses.Contracts.Providers;

public interface IObjectIdProvider
{
    Task<ObjectId[]> List(UserId userId, ObjectType objectType, Relation relation);
}