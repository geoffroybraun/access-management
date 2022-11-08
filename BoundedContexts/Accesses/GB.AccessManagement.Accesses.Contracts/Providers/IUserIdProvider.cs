using GB.AccessManagement.Accesses.Contracts.ValueTypes;
using GB.AccessManagement.Core.ValueTypes;

namespace GB.AccessManagement.Accesses.Contracts.Providers;

public interface IUserIdProvider
{
    Task<UserId[]> List(ObjectType objectType, ObjectId objectId, Relation relation);
}