using GB.AccessManagement.Accesses.Contracts.ValueTypes;
using GB.AccessManagement.Core.ValueTypes;

namespace GB.AccessManagement.Accesses.Contracts.Providers;

public interface IRecursiveUserIdProvider
{
    Task<UserId[]> Expand(ObjectType objectType, ObjectId objectId, Relation relation);
}